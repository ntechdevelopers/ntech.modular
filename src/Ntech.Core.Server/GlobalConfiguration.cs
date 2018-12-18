using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Ntech.Core.Server
{
    public static class GlobalConfiguration
    {
        static GlobalConfiguration()
        {
            Modules = new List<ModuleInfo>();
        }

        public static IList<ModuleInfo> Modules { get; set; }

        public static IServiceProvider ServiceProvider { get; set; }

        public static IHostingEnvironment Environment { get; set; }
    }
}
