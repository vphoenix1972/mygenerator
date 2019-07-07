﻿using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TemplateProject.Core.Interfaces.DataAccess;

namespace TemplateProject.DataAccess.MongoDB
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddMongoDB(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();

            var connectionUrl = new MongoUrl(connectionString);
            services.AddSingleton(_ => new MongoClient(connectionUrl));
            services.AddSingleton(sp => sp.GetService<MongoClient>().GetDatabase(connectionUrl.DatabaseName));
        }
    }
}
