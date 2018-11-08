﻿using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.DataAccess;
using TemplateProject.Utils;
using TemplateProject.Web.Common.ExceptionLogger;
using TemplateProject.Web.Configuration;
using TemplateProject.Web.Security;

namespace TemplateProject.Web
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

            services.AddWebSecurity(_config);

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

            app.UseSecurity();
            
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

                DeleteExpiredRefreshTokens(scope.ServiceProvider);
            }
        }

        private void MigrateDatabaseToLatestVersion(IServiceProvider provider)
        {
            var db = provider.GetRequiredService<IDatabaseService>();

            db.MigrateToLatestVersion();
        }

        private void DeleteExpiredRefreshTokens(IServiceProvider provider)
        {
            var db = provider.GetRequiredService<IDatabaseService>();

            var utcNow = DateTime.UtcNow;

            db.RefreshTokensRepository.DeleteExpired(utcNow);

            db.SaveChanges();
        }
    }
}