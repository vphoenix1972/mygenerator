using System;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateProject.Utils.Factories
{
    public class Factory<T, TImpl> : IFactory<T> where TImpl : T
    {
        private readonly IServiceProvider _provider;

        public Factory(IServiceProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            _provider = provider;
        }

        public T Create(params object[] parameters)
        {
            return ActivatorUtilities.CreateInstance<TImpl>(_provider, parameters);
        }
    }
}