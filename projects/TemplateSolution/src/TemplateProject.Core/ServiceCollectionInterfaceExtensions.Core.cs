using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Domain;
using TemplateProject.Utils.Factories;

namespace TemplateProject.Core
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddAutoMapper();

            services.AddSingleton<IFactory<ITodoItem>, Factory<ITodoItem, TodoItem>>();
            services.AddSingleton<IFactory<TodoItem>, Factory<TodoItem>>();
        }
    }
}