using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.DataAccess;
using <%= projectNamespace %>.Utils;
using <%= projectNamespace %>.Web.Common.ExceptionLogger;
using <%= projectNamespace %>.Web.Configuration;

namespace <%= projectNamespace %>.Web
{
    public class Startup
    {
        private readonly WebConfiguration _config;

        public Startup()
        {
            _config = new WebConfiguration(WebConstants.ConfigPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebConfiguration();

            services.AddUtils();
            services.AddCore();
            services.AddDataAccess(_config.DbConnectionString);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IApplicationLifetime applicationLifetime)
        {
            app.UseExceptionLogger();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapSpaFallbackRoute("spaFallback", new { controller = "Spa", action = "Index" });
            });

            applicationLifetime.ApplicationStarted.Register(() => OnApplicationStarted(app));
        }

        private void OnApplicationStarted(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                MigrateDatabaseToLatestVersion(scope.ServiceProvider);
            }
        }

        private void MigrateDatabaseToLatestVersion(IServiceProvider provider)
        {
            var db = provider.GetRequiredService<IDatabaseService>();

            db.MigrateToLatestVersion();
        }
    }
}