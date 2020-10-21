using Newtonsoft.Json;
using System.IdentityModel.Tokens;

namespace bUtility.TokenCache.Implementation
{
    internal static class Extensions
    {
        internal static string Serialize(this SessionSecurityToken sessionSecurityToken, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            if (sessionSecurityToken == null)
                return null;
            return JsonConvert.SerializeObject(sessionSecurityToken, settings).ToString();
        }

        internal static SessionSecurityToken Deserialize(this string sessionSecurityToken, Newtonsoft.Json.JsonSerializerSettings settings)
        {
            if (sessionSecurityToken == null)
                return null;
            return JsonConvert.DeserializeObject<SessionSecurityToken>(sessionSecurityToken, settings);
        }
    }
}
