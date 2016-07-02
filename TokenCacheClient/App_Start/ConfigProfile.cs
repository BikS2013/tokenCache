using System;
using System.Configuration;

namespace TokenCacheClient
{
    public class ConfigProfile
    {
        public string TokenCacheConnection { get; private set; }
        public string InternetBankingAuditConnection { get; private set; }

        public bool RequireHttps { get; private set; }

        public bool EnforceUniqueUserLogin { get; private set; }


        // Emulation Service

        public int RollingExpiryWindowInMinutes { get; set; }
        public static ConfigProfile Current { get; private set; }


        internal static ConfigProfile LoadConfigurationProfile()
        {
            var cp = new ConfigProfile
            {
                RequireHttps = LoadBooleanValue("RequireHttps"),

                EnforceUniqueUserLogin = LoadBooleanValue("EnforceUniqueUserLogin"),
                RollingExpiryWindowInMinutes = LoadIntValue("RollingExpiryWindowInMinutes", 65),
                TokenCacheConnection = ConfigurationManager.AppSettings["tokenCacheConnection"]

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

        public static string Clear(string value)
        {
            if (value == null) return null;
            if (value.Trim() == "") return null;
            return value.Trim();
        }


        private static TEnum LoadEnumValue<TEnum>(string enumValue)
            where TEnum : struct
        {
            var configValue = ConfigurationManager.AppSettings[enumValue];
            TEnum value;
            if (Enum.TryParse(configValue, true, out value))
                return value;

            if (String.IsNullOrEmpty(enumValue))
                throw new ConfigurationErrorsException("Null value not valid for " + typeof(TEnum) + " type");

            throw new ConfigurationErrorsException("Value " + enumValue + " not valid for " + typeof(TEnum) + " type");
        }

    }
}