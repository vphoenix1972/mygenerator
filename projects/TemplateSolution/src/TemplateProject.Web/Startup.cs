using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using TemplateProject.Core;
using TemplateProject.Core.Interfaces.DataAccess;
using TemplateProject.DataAccess.PostgreSQL;
using TemplateProject.Utils;
using TemplateProject.Web.Common.ExceptionLogger;
using TemplateProject.Web.Configuration;
using TemplateProject.Web.Security;

namespace TemplateProject.Web
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly WebConfiguration _config;

        public Startup(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Startup>();
            _config = new WebConfiguration(WebConstants.ConfigPath);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(
                AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("TemplateProject"))
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
            var timePassed = TimeSpan.Zero;
            var delay = TimeSpan.FromSeconds(1);

            do
            {
                var isStartupProcedureSuccessfull = true;

                try
                {
                    PerformStartup(app);
                }
                catch (Exception e)
                {
                    _logger.LogCritical(e, "Startup procedure has failed.");

                    isStartupProcedureSuccessfull = false;
                }

                if (isStartupProcedureSuccessfull)
                    break;

                Thread.Sleep(delay);

                timePassed = timePassed.Add(delay);
            } while (timePassed < WebConstants.MaxAllowedTimeToPerformStartup);
        }

        private void PerformStartup(IApplicationBuilder app)
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