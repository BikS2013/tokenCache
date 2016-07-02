using System;

namespace bUtility.Services.Controllers.TokenCache
{
    public class Registration
    {
        public static void Register(Func<PersistentLib.ISqlFactory> sqlConnector, Newtonsoft.Json.JsonSerializerSettings serializationSettings, int rollingExpiryWindowInMinutes)
        {
        }
    }
}
