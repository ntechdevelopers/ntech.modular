using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Infrastructure;
using Ntech.Modules.Core;
using Ntech.Platform.Repository;

namespace Ntech.WebHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ModuleAPIDataContext>();
                    DbInitializer.Initialize(context);

                    var contextIdentity = services.GetRequiredService<CoreDbContext>();
                    IdentityInitializer.Initialize(contextIdentity);
                }
                catch (System.Exception ex)
                {
                    // Use check error, but ignore exception.
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return Microsoft.AspNetCore.WebHost.CreateDefaultBuilder(args)
                          .UseKestrel()
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseIISIntegration()
                          .UseStartup<Startup>();
        }
    }
}
