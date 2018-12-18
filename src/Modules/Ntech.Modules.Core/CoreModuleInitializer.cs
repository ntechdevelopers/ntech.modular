using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Core.Server;
using Ntech.Modules.Core.Models;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Text;
using System.Threading.Tasks;

namespace Ntech.Modules.Core
{
    [Export("Ntech.Modules.Core", typeof(IModule))]
    public class CoreModuleInitializer : IModuleInitializer, IModule
    {
        public void Init(IServiceCollection serviceCollection, IConfiguration configuration)
        {
        }

    }
}
