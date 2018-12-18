using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Ntech.Core.Server;
using Ntech.Modules.Core.Models;
using Ntech.WebHost.Extensions;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Ntech.Platform.Repository;
using System.Composition;
using System.Composition.Hosting;

namespace Ntech.WebHost
{
    public class Startup
    {
        private readonly IHostingEnvironment hostingEnvironment;

        private readonly IList<ModuleInfo> modules = new List<ModuleInfo>();

        public Startup(IHostingEnvironment env)
        {
            try
            {
                hostingEnvironment = env;

                var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

                builder.AddEnvironmentVariables();
                Configuration = builder.Build();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //LoadInstalledModules();

            this.Compose();

            services.AddDbContext<Modules.Core.CoreDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Ntech.WebHost")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<Modules.Core.CoreDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                var viewEngine = new ModuleViewLocationExpander();
                options.FileProviders.Add(new PhysicalFileProvider(Path.Combine(Environment.CurrentDirectory, @"../")));
                options.ViewLocationExpanders.Add(viewEngine);
            });

            var mvcBuilder = services.AddMvc();
            var moduleInitializerInterface = typeof(IModuleInitializer);
            var moduleClientInitializerInterface = typeof(IClientsModuleInitializer);
            foreach (var module in modules)
            {
                // Register controller from modules
                mvcBuilder.AddApplicationPart(module.Assembly);

                // Register dependency in modules
                var moduleInitializerType = module.Assembly.GetTypes().Where(x => moduleInitializerInterface.IsAssignableFrom(x)).FirstOrDefault();
                if (moduleInitializerType != null && moduleInitializerType != moduleInitializerInterface)
                {
                    var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                    moduleInitializer.Init(services, this.Configuration);
                }

                // Register dependency in modules client
                var moduleClientInitializerType = module.Assembly.GetTypes().Where(x => moduleClientInitializerInterface.IsAssignableFrom(x)).FirstOrDefault();
                if (moduleClientInitializerType != null && moduleClientInitializerType != moduleClientInitializerInterface)
                {
                    var moduleClientInitializer = (IClientsModuleInitializer)Activator.CreateInstance(moduleClientInitializerType);
                    moduleClientInitializer.InitService(services, module.Path);
                }

                //// Merge view engine
                ////Create an EmbeddedFileProvider for that assembly
                //var embeddedFileProvider = new EmbeddedFileProvider(
                //    module.Assembly,
                //    module.ShortName
                //);

                ////Add the file provider to the Razor view engine
                //services.Configure<RazorViewEngineOptions>(options =>
                //{
                //    options.FileProviders.Add(embeddedFileProvider);
                //});
            }

            // TODO: break down to new method in new class
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(RepositoryWithTypedId<,>)).As(typeof(IRepositoryWithTypedId<,>));
            foreach (var module in GlobalConfiguration.Modules)
            {
                builder.RegisterAssemblyTypes(module.Assembly).AsImplementedInterfaces();
            }

            builder.RegisterInstance(Configuration);
            builder.RegisterInstance(hostingEnvironment);
            builder.Populate(services);
            var container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            // Serving static file for modules
            foreach (var module in modules)
            {
                var wwwrootDir = new DirectoryInfo(Path.Combine(module.Path, "wwwroot"));
                if (!wwwrootDir.Exists)
                {
                    continue;
                }

                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(wwwrootDir.FullName),
                    RequestPath = new PathString("/" + module.ShortName)
                });
            }

            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            foreach (var module in modules)
            {
                var sourcePath = Path.Combine(Environment.CurrentDirectory, @"../", "Modules", module.Name);
                var clientApp = new DirectoryInfo(Path.Combine(sourcePath, "ClientApp"));
                if (clientApp.Exists)
                {
                    // Register dependency in modules
                    var moduleInitializerType = module.Assembly.GetTypes().Where(x => typeof(IClientsModuleInitializer).IsAssignableFrom(x)).FirstOrDefault();
                    if (moduleInitializerType != null && moduleInitializerType != typeof(IClientsModuleInitializer))
                    {
                        var moduleInitializer = (IClientsModuleInitializer)Activator.CreateInstance(moduleInitializerType);
                        moduleInitializer.Init(app, clientApp.FullName, this.Configuration);
                    }
                }
            }

            CreateRoles(serviceProvider, Configuration).Wait();
        }

        private void LoadInstalledModules()
        {
            var moduleRootFolder = new DirectoryInfo(Path.Combine(hostingEnvironment.ContentRootPath, "Modules"));
            if (!moduleRootFolder.Exists)
            {
                return;
            }

            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }

                foreach (var file in binFolder.GetFileSystemInfos("*.dll", SearchOption.AllDirectories))
                {
                    Assembly assembly = null;
                    try
                    {
                        assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    }
                    catch (FileLoadException ex)
                    {
                        if (ex.Message == "Assembly with same name is already loaded")
                        {
                            // Get loaded assembly
                            assembly = Assembly.Load(new AssemblyName(Path.GetFileNameWithoutExtension(file.Name)));
                        }
                        else
                        {
                            throw;
                        }
                    }

                    if (assembly.FullName.Contains(moduleFolder.Name))
                    {
                        modules.Add(new ModuleInfo { Name = moduleFolder.Name, Assembly = assembly, Path = moduleFolder.FullName });
                    }
                }
            }

            GlobalConfiguration.Modules = modules;
        }

        [ImportMany]
        public IEnumerable<IModule> ModuleComposer { get; set; }
        private void Compose()
        {
            var executableLocation = Assembly.GetEntryAssembly().Location;
            var path = Path.Combine(Path.GetDirectoryName(executableLocation));
            var assemblies = Directory.GetFiles(path, "Ntech.Modules.*.dll", SearchOption.AllDirectories)
                                      .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath).ToList();
            foreach (var assembly in assemblies)
            {
                Assembly dependencyAssembly = null;
                try
                {
                    dependencyAssembly = Assembly.LoadFrom(assembly.Location);
                    //dependencyAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assembly.Location);
                }
                catch (FileLoadException ex)
                {
                    if (ex.Message == "Assembly with same name is already loaded")
                    {
                        dependencyAssembly = Assembly.Load(assembly.GetName());
                    }
                    else
                    {
                        throw;
                    }
                }
                modules.Add(new ModuleInfo { Name = dependencyAssembly.GetName().Name, Assembly = dependencyAssembly, Path = dependencyAssembly.Location });
            }

            GlobalConfiguration.Modules = modules;

            var configuration = new ContainerConfiguration().WithAssemblies(assemblies);
            using (var container = configuration.CreateContainer())
            {
                ModuleComposer = container.GetExports<IModule>();
            }
        }

        private async Task CreateRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            try
            {
                //adding custom roles
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
                string[] roleNames = { "Admin", "Manager", "Member" };
                foreach (var roleName in roleNames)
                {
                    //creating the roles and seeding them to the database
                    var roleExist = await RoleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        var roleResult = await RoleManager.CreateAsync(new Role(roleName));
                    }
                }
                //creating a super user who could maintain the web app
                var poweruser = new User
                {
                    UserName = configuration.GetSection("UserSettings").GetValue<string>("UserEmail"),
                    Email = configuration.GetSection("UserSettings").GetValue<string>("UserEmail")
                };
                string UserPassword = configuration.GetSection("UserSettings").GetValue<string>("UserPassword");
                var _user = await UserManager.FindByEmailAsync(configuration.GetSection("UserSettings").GetValue<string>("UserEmail"));
                if (_user == null)
                {
                    var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                    if (createPowerUser.Succeeded)
                    {
                        //here we tie the new user to the "Admin" role 
                        await UserManager.AddToRoleAsync(poweruser, "Admin");
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
}
