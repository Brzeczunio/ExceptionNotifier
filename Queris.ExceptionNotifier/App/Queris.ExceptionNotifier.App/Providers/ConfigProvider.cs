using System;
using System.IO;

namespace Queris.ExceptionNotifier.App.Providers
{
    internal static class ConfigProvider
    {
        internal static bool Log4NetLogger => Common.Providers.ConfigProvider.GetBool("Log4NetLogger");
        internal static int Log4NetLoggerDaysToKeepLogs => Common.Providers.ConfigProvider.GetInt("Log4NetLoggerDaysToKeepLogs");
        internal static int SendNotificationInterval => Common.Providers.ConfigProvider.GetInt("SendNotificationInterval");
        internal static string FileName => GetFileName();
        internal static DateTime ApplicationLifetime => Common.Providers.ConfigProvider.GetDateTime("ApplicationLifetime");
        internal static int CheckApplicationLifetimeInterval => Common.Providers.ConfigProvider.GetInt("CheckApplicationLifetimeInterval");

        private static string GetFileName()
        {
            var fileName = Common.Providers.ConfigProvider.GetString("FileName");
            if(string.IsNullOrWhiteSpace(fileName)) throw new ArgumentException("Enter the path to the Config.json file");

            if(!File.Exists(fileName)) throw new ArgumentException("The entered path does not exist!");
            return fileName;
        }
    }
}