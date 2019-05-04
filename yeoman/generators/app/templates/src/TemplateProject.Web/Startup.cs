using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using <%= projectNamespace %>.Core;
using <%= projectNamespace %>.Core.Interfaces.DataAccess;
using <%= projectNamespace %>.DataAccess.PostgreSQL;
using <%= projectNamespace %>.Utils;
using <%= projectNamespace %>.Web.Common.ExceptionLogger;
using <%= projectNamespace %>.Web.Configuration;
using <%= projectNamespace %>.Web.Security;

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
            services.AddAutoMapper(
                AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("<%= projectNamespace %>"))
            );

            services.AddWebConfiguration();

            services.AddUtils();
            services.AddCore();

            /* Change database backend here */
            services.AddPostgreSQL(_config.DbConnectionString);

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
            var config = provider.GetRequiredService<IWebConfiguration>();

            var beforeUtc = DateTime.UtcNow - config.JwtClockSkew;

            db.RefreshTokensRepository.DeleteExpired(beforeUtc);

            db.SaveChanges();
        }
    }
}