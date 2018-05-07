using System;
using System.Configuration;

namespace Queris.ExceptionNotifier.Common.Providers
{
    public static class ConfigProvider
    {
        public static bool GetBool(string key)
        {
            return bool.TryParse(ConfigurationManager.AppSettings[key], out bool result) && result;
        }

        public static string GetString(string key)
        {
            return GetString(key, "");
        }

        public static string GetString(string key, string defaultValue)
        {
            var result = ConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(result) ? defaultValue : result;
        }

        public static int GetInt(string key)
        {
            return GetInt(key, default(int));
        }

        public static int GetInt(string key, int defaultValue)
        {
            return int.TryParse(ConfigurationManager.AppSettings[key], out int result) ? result : defaultValue;
        }

        public static string GetConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings["key"].ConnectionString;
        }

        public static TimeSpan GetTime(string key)
        {
            return TimeSpan.TryParse(ConfigurationManager.AppSettings[key], out TimeSpan result) ? result : TimeSpan.Zero;
        }

        public static DateTime GetDateTime(string key)
        {
            return DateTime.TryParse(ConfigurationManager.AppSettings[key], out DateTime result) ? result : DateTime.MinValue;
        }
    }
}