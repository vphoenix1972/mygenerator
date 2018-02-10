using Microsoft.Extensions.DependencyInjection;
using TemplateProject.Core.Domain;
using TemplateProject.Utils.Factories;

namespace TemplateProject.Core
{
    public static class ServiceCollectionInterfaceExtensions
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IFactory<ITodoItem>, Factory<ITodoItem, TodoItem>>();

            services.AddSingleton<IFactory<IUser>, Factory<IUser, User>>();
            services.AddSingleton<IFactory<User>, Factory<User, User>>();

            services.AddSingleton<IFactory<UserRole>, Factory<UserRole, UserRole>>();
        }
    }
}