using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

        public static XmlElement GetFirst( this XmlNodeList nodeList, string errorMessage = null)
        {
            if ( (nodeList?.Count ?? 0) == 0)
            {
                throw new ConfigurationErrorsException(errorMessage ?? "No element found in nodeList.");
            }

            XmlElement element = nodeList.Item(0) as XmlElement;
            return element;
        }

        public static string GetStringAttribute(this XmlElement elm, string attrName, string errorMessage = null)
        {
            string attrValue;
            if (elm.Attributes[attrName] != null)
            {
                attrValue = elm.Attributes[attrName].Value;
            }
            else
            {
                throw new ConfigurationErrorsException(errorMessage ?? $"<{elm.LocalName}> is expected to contain a '{attrName}' attribute.");
            }
            return attrValue;
        }
        public static int GetIntAttribute(this XmlElement elm, string attrName, string errorMessage = null)
        {
            int attrValue;
            if (elm.Attributes[attrName] != null)
            {
                Int32.TryParse(elm.Attributes[attrName].Value, out attrValue);
            }
            else
            {
                throw new ConfigurationErrorsException(errorMessage ?? $"<{elm.LocalName}> is expected to contain a '{attrName}' attribute.");
            }
            return attrValue;
        }
    }
}
