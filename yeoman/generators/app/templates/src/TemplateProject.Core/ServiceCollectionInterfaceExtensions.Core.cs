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
            services.AddSingleton<IFactory<TodoItem>, Factory<TodoItem>>();

            services.AddSingleton<IFactory<IUser>, Factory<IUser, User>>();
            services.AddSingleton<IFactory<User>, Factory<User>>();
            services.AddSingleton<IFactory<UserRole>, Factory<UserRole>>();

            services.AddSingleton<IFactory<IRefreshToken>, Factory<IRefreshToken, RefreshToken>>();
            services.AddSingleton<IFactory<RefreshToken>, Factory<RefreshToken>>();

        }
    }
}