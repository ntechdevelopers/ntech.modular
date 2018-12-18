using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Core.Server;
using Ntech.Platform.CommonService;
using Ntech.Platform.Repository;
using System.Composition;

namespace Ntech.Modules.Api.Base
{
    [Export("Ntech.Modules.Api.Base", typeof(IModule))]
    public class ApiModuleInitializer : IModuleInitializer, IModule
    {
        public void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommonService, CommonService>();

            // BaseDataContext
            services.AddDbContext<ModuleAPIDataContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("ModuleConnection"), b
                                        => b.MigrationsAssembly("Ntech.Modules.*")));
            // Unit of work
            services.AddScoped(typeof(IUnitOfWorkSubDatabase), typeof(ModuleAPIUnitOfWork));
        }
    }
}
