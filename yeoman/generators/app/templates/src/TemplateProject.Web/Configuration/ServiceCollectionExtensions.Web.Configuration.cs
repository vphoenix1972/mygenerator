﻿using Microsoft.Extensions.DependencyInjection;

namespace <%= projectNamespace %>.Web.Configuration
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddWebConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IWebConfiguration>(
                p => ActivatorUtilities.CreateInstance<WebConfiguration>(p, WebConstants.ConfigPath));
        }
    }
}