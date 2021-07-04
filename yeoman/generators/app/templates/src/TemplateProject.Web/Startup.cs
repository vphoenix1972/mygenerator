﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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
                AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("<%= projectNamespace %>"))
            );

            services.AddWebConfiguration();

            services.AddUtils();
            services.AddCore();

            /* Change database backend here */
            services.AddPostgreSQL(_config.DbConnectionString);

            services.AddWebSecurity(_config);

            services.AddControllers();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "REST API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                x.IncludeXmlComments(xmlPath);

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[0]
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime)
        {
            app.UseExceptionLogger();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "REST API V1");
            });

            app.UseRouting();

            app.UseSecurity();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Spa");
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