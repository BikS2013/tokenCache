using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache
{
    public static class Extensions
    {
        public static string Clear(this string value)
        {
            if (value == null) return null;
            if (value.Trim() == "") return null;
            return value.Trim();
        }

        public static string SerializeSecurityToken(this SecurityToken securityToken, SecurityTokenSerializer securityTokenSerializer)
        {
            if (securityTokenSerializer == null)
                return null;
            return securityTokenSerializer.SerializeToken(securityToken);
        }

        public static SecurityToken DeserializeSecurityToken(this string securityToken, SecurityTokenSerializer securityTokenSerializer)
        {
            if (securityTokenSerializer == null)
                return null;
            return securityTokenSerializer.DeserializeToken(securityToken);
        }

        private static void SetJsonSerializationSettings(Newtonsoft.Json.JsonSerializerSettings settings)
        {
            settings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }
        public static Newtonsoft.Json.JsonSerializerSettings GetSerializationSettings()
        {
            Newtonsoft.Json.JsonSerializerSettings settings = new Newtonsoft.Json.JsonSerializerSettings();
            SetJsonSerializationSettings(settings);
            return settings;
        }

    }
}
