using System;

namespace <%= projectNamespace %>.Web
{
    public static class WebConstants
    {
        public const string NLogConfigPath = "NLog.config";
        public const string ConfigPath = "appsettings.json";

        public const string DefaultServerUrls = "http://*:8888";

        public const string ApiPrefix = "api";

        public static readonly TimeSpan MaxAllowedTimeToPerformStartup = TimeSpan.FromSeconds(30);

        public const int MaxLimit = 1000;
    }
}