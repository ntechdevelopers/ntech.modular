using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Core.Server;
using Ntech.Modules.DashboardModule.Services;
using Ntech.Platform.CommonService;
using System;
using System.Composition;

namespace Ntech.Modules.DashboardModule
{
    [Export("Ntech.Modules.DashboardModule", typeof(IModule))]
    public class DashBoardModuleInitializer : IModuleInitializer, IModule
    {
        public void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IDashboardService, DashboardService>();
        }
    }
}
