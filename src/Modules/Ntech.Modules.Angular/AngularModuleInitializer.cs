using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ntech.Core.Server;
using System;
using System.Composition;
using System.IO;

namespace Ntech.Modules.Angular
{
    [Export("Ntech.Modules.Angular", typeof(IModule))]
    public class AngularModuleInitializer : IClientsModuleInitializer, IModule
    {
        public void Init(IApplicationBuilder app, string sourcePath, IConfiguration configuration)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = sourcePath;//Path.Combine(sourcePath, "Angular");
                spa.UseAngularCliServer(npmScript: "start");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "spa-fallback",
                    template: "{*url}",
                    defaults: new { controller = "Angular", action = "Index" });

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback-angular",
                    defaults: new { controller = "Angular", action = "Index" });
            });
        }

        public void InitService(IServiceCollection services, string modulePath)
        {
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = Path.Combine(modulePath, @"ClientApp\dist");
            });

        }
    }
}
