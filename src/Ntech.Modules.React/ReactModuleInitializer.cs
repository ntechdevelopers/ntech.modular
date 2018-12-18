using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Core.Server;
using System;
using System.IO;

namespace Ntech.Modules.React
{
    public class ReactModuleInitializer : IClientsModuleInitializer
    {
        public void Init(IApplicationBuilder app, string sourcePath, IConfiguration configuration)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = sourcePath; // Path.Combine(sourcePath, "React");

                spa.UseReactDevelopmentServer(npmScript: "start");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "React", action = "Index" });

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback-react",
                    defaults: new { controller = "React", action = "Index" });
            });
        }

        public void InitService(IServiceCollection services, string modulePath)
        {
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = Path.Combine(modulePath, @"ClientApp\build");
            });

        }
    }
}
