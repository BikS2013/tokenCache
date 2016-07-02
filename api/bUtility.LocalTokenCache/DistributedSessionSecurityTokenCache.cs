using bUtility.TokenCache.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace bUtility.LocalTokenCache
{
    public class DistributedSessionSecurityTokenCache : 
        System.IdentityModel.Tokens.SessionSecurityTokenCache,
        System.IdentityModel.Configuration.ICustomIdentityConfiguration
    {
        private static object locker = new object();
        static string applicationId = "61A1195E-7BD3-4EC2-82BC-82582306F8D8";

        public static string ApplicationId
        {
            get
            {
                lock (locker)
                {
                    return applicationId;
                }
            }
            set
            {
                lock (locker)
                {
                    applicationId = value;
                }
            }
        }

        private static RecentlyUsedSessionSecurityTokenCache _internalCache;
        private static IDistributedSessionSecurityTokenCache cache;

        public override void AddOrUpdate(System.IdentityModel.Tokens.SessionSecurityTokenCacheKey key,
            System.IdentityModel.Tokens.SessionSecurityToken value, DateTime expiryTime)
        {
            string tokenId = null;
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity != null && claimsIdentity.BootstrapContext != null)
            {
                var bootstrap = claimsIdentity.BootstrapContext as BootstrapContext;
                if (bootstrap != null && bootstrap.SecurityToken != null)
                    tokenId = bootstrap.SecurityToken.Id;
            }

            var data = cache.AddOrUpdate(new bUtility.TokenCache.Types.SessionSecurity.SessionCacheEntry
            {
                EndpointId = key.EndpointId,
                ContextId = GetContextIdString(key),
                KeyGeneration = GetKeyGenerationString(key),
                ExpiryTime = expiryTime,
                SessionSecurityTokenValue = value,
                UserName = Thread.CurrentPrincipal.Identity.Name,
                SessionSecurityTokenID = tokenId
            });

            if (data)
                _internalCache.AddOrUpdate(key, value, expiryTime);
        }

        public override SessionSecurityToken Get(SessionSecurityTokenCacheKey key)
        {
            var resLocal = _internalCache.Get(key);
            if (resLocal == null)
            {
                var token = cache.Get(new bUtility.TokenCache.Types.SessionSecurity.SessionCacheKey
                    {
                        EndpointId = key.EndpointId,
                        ContextId = GetContextIdString(key),
                        KeyGeneration = GetKeyGenerationString(key)
                    });
                if (token != null)
                {
                    resLocal = token;
                    _internalCache.AddOrUpdate(key, token, token.KeyExpirationTime);
                }
            }
            return resLocal;
        }

        public override IEnumerable<SessionSecurityToken> GetAll(string endpointId, UniqueId contextId)
        {
#warning perhaps implement this for in memory
            var data = cache.GetAll(new bUtility.TokenCache.Types.SessionSecurity.Context
                {
                    EndpointId = endpointId,
                    ContextId = GetContextIdString(contextId)
                });
            foreach (var token in data)
            {
                SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
                key.IgnoreKeyGeneration = true;
                _internalCache.AddOrUpdate(key, token, token.KeyExpirationTime);
            }
            return data;
        }

        public override void Remove(SessionSecurityTokenCacheKey key)
        {
            _internalCache.Remove(key);
            cache.Remove(new bUtility.TokenCache.Types.SessionSecurity.SessionCacheKey
            {
                    EndpointId = key.EndpointId,
                    ContextId = GetContextIdString(key),
                    KeyGeneration = GetKeyGenerationString(key)
                });
        }

        public override void RemoveAll(string endpointId)
        {
            _internalCache.RemoveAll(endpointId);
            cache.RemoveAllByEndpointId(new bUtility.TokenCache.Types.SessionSecurity.Endpoint
                { EndpointId = endpointId });
        }

        public override void RemoveAll(string endpointId, UniqueId contextId)
        {
            _internalCache.RemoveAll(endpointId, contextId);

            cache.RemoveAll(new bUtility.TokenCache.Types.SessionSecurity.Context
            {
                    EndpointId = endpointId,
                    ContextId = GetContextIdString(contextId)
                });

        }

        void ICustomIdentityConfiguration.LoadCustomConfiguration(XmlNodeList nodeList)
        {
            // Retrieve the endpoint address of the centralized session security token cache service running in the web farm
            if (nodeList.Count == 0)
            {
                throw new ConfigurationErrorsException("No child config element found under <sessionSecurityTokenCache>.");
            }

            XmlElement cacheServiceAddressElement = nodeList.Item(0) as XmlElement;
            if (cacheServiceAddressElement == null || cacheServiceAddressElement.LocalName != "distributedCacheConfiguration")
            {
                throw new ConfigurationErrorsException("First child config element under <sessionSecurityTokenCache> is expected to be <distributedCacheConfiguration>.");
            }

            string cacheServiceAddress;
            if (cacheServiceAddressElement.Attributes["connectionString"] != null)
            {
                cacheServiceAddress = cacheServiceAddressElement.Attributes["connectionString"].Value;
            }
            else
            {
                throw new ConfigurationErrorsException("<cacheServiceAddress> is expected to contain a 'connectionString' attribute.");
            }

            int maxCacheSize = 0;
            if (cacheServiceAddressElement.Attributes["maxCacheSize"] != null)
            {
                Int32.TryParse(cacheServiceAddressElement.Attributes["maxCacheSize"].Value, out maxCacheSize);
            }

            // Initialize the proxy to the WebFarmSessionSecurityTokenCacheService
            Initialize(cacheServiceAddress, maxCacheSize);
        }

        private static void Initialize(string cacheServiceAddress, int maxCacheSize)
        {
            cache = new bUtility.TokenCache.Implementation.DistributedSessionSecurityTokenCache(
                () => { return new PersistentLib.SqlServerFactory(cacheServiceAddress); }, bUtility.TokenCache.Extensions.GetSerializationSettings(), 60 );

            _internalCache = new RecentlyUsedSessionSecurityTokenCache(maxCacheSize);
        }
        private static string GetKeyGenerationString(SessionSecurityTokenCacheKey key)
        {
            return key.KeyGeneration == null ? null : key.KeyGeneration.ToString();
        }

        private static string GetContextIdString(SessionSecurityTokenCacheKey key)
        {
            return GetContextIdString(key.ContextId);
        }

        private static string GetContextIdString(UniqueId contextId)
        {
            return contextId == null ? null : contextId.ToString();
        }


    }
}
