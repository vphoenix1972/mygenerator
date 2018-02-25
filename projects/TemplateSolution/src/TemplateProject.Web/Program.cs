using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using TemplateProject.Web.Configuration;

namespace TemplateProject.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog(WebConstants.NLogConfigPath).GetCurrentClassLogger();
            try
            {
                logger.Info("Init main");
                BuildWebHost(args).Run();
            }
            catch (Exception e)
            {
                logger.Error(e, "Stopped program because of exception");
                throw;
            }
        }

        private static IWebHost BuildWebHost(string[] args)
        {
            var config = new WebConfiguration(WebConstants.ConfigPath);

            return WebHost.CreateDefaultBuilder(args)
                .UseNLog()
                .UseStartup<Startup>()
                .UseUrls(config.ServerUrls)
                .Build();
        }
    }
}