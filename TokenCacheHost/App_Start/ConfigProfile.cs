using System;
using System.Configuration;

namespace TokenCacheHost
{
    public class ConfigProfile
    {
        public static ConfigProfile Current { get; private set; }
        public string TokenCacheConnection { get; set; }
        public bool RequireHttps { get; set; }
        public int RollingExpiryWindowInMinutes { get; set; }


        internal static ConfigProfile LoadConfigurationProfile()
        {
            var cp = new ConfigProfile
            {
                TokenCacheConnection = ConfigurationManager.AppSettings["tokenCacheConnection"],
                RequireHttps = LoadBooleanValue("RequireHttps"),

                RollingExpiryWindowInMinutes = LoadIntValue("rollingExpiringWindowInMinutes", 65),

            };
            Current = cp;
            return cp;
        }


        private static bool LoadBooleanValue(string key, bool defaultValue = false)
        {
            var configValue = ConfigurationManager.AppSettings[key];
            bool value;
            if (bool.TryParse(configValue, out value))
                return value;

            return defaultValue;
        }
        public static int LoadIntValue(string key, int defaultValue = 0)
        {
            var configValue = ConfigurationManager.AppSettings[key];
            int value;
            if (int.TryParse(configValue, out value))
                return value;

            return defaultValue;
        }
    }
}