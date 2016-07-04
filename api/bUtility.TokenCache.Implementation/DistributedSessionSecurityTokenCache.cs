using bUtility.TokenCache.Types.SessionSecurity;
using bUtility.TokenCache.Data;
using Newtonsoft.Json;
using PersistentLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using bUtility.TokenCache.Interfaces;

namespace bUtility.TokenCache.Implementation
{
    public class DistributedSessionSecurityTokenCache : IDistributedSessionSecurityTokenCache
    {
        public delegate int RollingExpirationProvider();

        private readonly Func<PersistentLib.ISqlFactory> _sqlConnector;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private readonly int _rollingExpiryWindowInMinutes;
        protected DistributedSessionSecurityTokenCache(Func<PersistentLib.ISqlFactory> sqlConnector, 
            JsonSerializerSettings jsonSerializerSettings, 
            int rollingExpiryWindowInMinutes)
        {
            _sqlConnector = sqlConnector;
            _jsonSerializerSettings = jsonSerializerSettings;
            _rollingExpiryWindowInMinutes = rollingExpiryWindowInMinutes;
        }

        public DistributedSessionSecurityTokenCache(Func<PersistentLib.ISqlFactory> sqlConnector,
            Func<JsonSerializerSettings> jsonSettingProvider, 
            RollingExpirationProvider expirationValueProvider): this(sqlConnector, jsonSettingProvider(), expirationValueProvider())
        {
        }

        private ISqlFactory GetSqlServerFactory()
        {
            return _sqlConnector();
        }

        public bool AddOrUpdate(SessionCacheEntry request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                var resp = SecurityTokenCache_Ex.AddOrUpdate(
                    sqlFactory,
                    request.EndpointId,
                    request.ContextId,
                    request.KeyGeneration,
                    JsonConvert.SerializeObject(request.SessionSecurityTokenValue, _jsonSerializerSettings).ToString(),
                    request.SessionSecurityTokenID,
                    request.ExpiryTime.ToLocalTime(),
                    DateTime.Now.AddMinutes(_rollingExpiryWindowInMinutes),
                    request.UserName);

                return resp;
            }
        }

        public IEnumerable<SessionSecurityToken> GetAll(Context request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                IEnumerable<string> tokens = SecurityTokenCache_Ex.GetAll(
                sqlFactory,
                request.EndpointId,
                request.ContextId);
                var deserializedTokens = new Collection<SessionSecurityToken>();
                foreach (var token in tokens)
                {
                    deserializedTokens.Add(
                        JsonConvert.DeserializeObject<SessionSecurityToken>(token, _jsonSerializerSettings)
                        );
                }
                return deserializedTokens;
            }
        }

        public SessionSecurityToken Get(SessionCacheKey request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                var token = SecurityTokenCache_Ex.Get(
                    sqlFactory,
                    request.EndpointId,
                    request.ContextId,
                    request.KeyGeneration);
                var response = JsonConvert.DeserializeObject<SessionSecurityToken>(token, _jsonSerializerSettings);
                return response;
            }
        }

        public bool Remove(SessionCacheKey request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {

                SecurityTokenCache_Ex.Remove(
                    sqlFactory,
                    request.EndpointId,
                    request.ContextId,
                    request.KeyGeneration);
            }
            return true;
        }

        public bool RemoveAllByEndpointId(Endpoint request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                SecurityTokenCache_Ex.RemoveAll(
                    sqlFactory,
                    request.EndpointId);
            }
            return true;
        }

        public bool RemoveAll(Context request)
        {
            using (var sqlFactory = GetSqlServerFactory())
            {
                SecurityTokenCache_Ex.RemoveAll(
                    sqlFactory,
                    request.EndpointId,
                    request.ContextId);
            }
            return true;
        }

        public static bool CheckForUserTokenAndUpdateRollingTimeout(Func<PersistentLib.ISqlFactory> connector, string user, string id, int rollingExpiryWindowInMinutes)
        {
            using (var sqlFactory = connector())
            {
                return SecurityTokenCache_Ex.CheckForUserTokenAndUpdateRollingTimeout(sqlFactory, user, id,
                    DateTime.Now.AddMinutes(rollingExpiryWindowInMinutes), DateTime.Now);
            }
        }
    }
}
