using System;

namespace <%= projectNamespace %>.Web.Configuration
{
    public interface IWebConfiguration
    {
        string ServerUrls { get; }

        string DbConnectionString { get; }
    }
}