using Microsoft.Extensions.DependencyInjection;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.Utils.Factories;

namespace <%= projectNamespace %>.Core
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IFactory<ITodoItem>, Factory<ITodoItem, TodoItem>>();
        }
    }
}