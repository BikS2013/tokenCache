using bUtility.TokenCache.Types.SessionSecurity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading;
using System.Xml;

namespace bUtility.RemoteTokenCache
{
    public class DistributedSessionSecurityTokenCache : SessionSecurityTokenCache, ICustomIdentityConfiguration
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



        static private RecentlyUsedSessionSecurityTokenCache _internalCache;

        private static string _apiBaseUrl = null;
        public override void AddOrUpdate(SessionSecurityTokenCacheKey key, SessionSecurityToken value, DateTime expiryTime)
        {


            string tokenId = null;
            ApiHelper helper = new ApiHelper(_apiBaseUrl);
            var claimsIdentity = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            if (claimsIdentity != null && claimsIdentity.BootstrapContext != null)
            {
                var bootstrap = claimsIdentity.BootstrapContext as BootstrapContext;
                if (bootstrap != null && bootstrap.SecurityToken != null)
                    tokenId = bootstrap.SecurityToken.Id;
            }
            var res = helper.AddOrUpdate(new SessionCacheEntry()
            {
                EndpointId = key.EndpointId,
                ContextId = GetContextIdString(key),
                KeyGeneration = GetKeyGenerationString(key),
                ExpiryTime = expiryTime,
                SessionSecurityTokenValue = value,
                UserName = Thread.CurrentPrincipal.Identity.Name,
                SessionSecurityTokenID = tokenId
            });
            if (res)
                _internalCache.AddOrUpdate(key, value, expiryTime);
        }

        public override SessionSecurityToken Get(SessionSecurityTokenCacheKey key)
        {
            var resLocal = _internalCache.Get(key);
            if (resLocal == null)
            {
                ApiHelper helper = new ApiHelper(_apiBaseUrl);

                var token = helper.Get(new SessionCacheKey()
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
            ApiHelper helper = new ApiHelper(_apiBaseUrl);

            var res = helper.GetAll(new Context()
            {
                EndpointId = endpointId,
                ContextId = GetContextIdString(contextId)
            });

            foreach (var token in res)
            {
                SessionSecurityTokenCacheKey key = new SessionSecurityTokenCacheKey(endpointId, contextId, null);
                key.IgnoreKeyGeneration = true;
                _internalCache.AddOrUpdate(key, token, token.KeyExpirationTime);
            }
            return res;
        }

        public override void Remove(SessionSecurityTokenCacheKey key)
        {
            _internalCache.Remove(key);

            ApiHelper helper = new ApiHelper(_apiBaseUrl);

            helper.Remove(new SessionCacheKey()
            {
                EndpointId = key.EndpointId,
                ContextId = GetContextIdString(key),
                KeyGeneration = GetKeyGenerationString(key)
            });
        }

        public override void RemoveAll(string endpointId)
        {
            _internalCache.RemoveAll(endpointId);

            ApiHelper helper = new ApiHelper(_apiBaseUrl);

            helper.RemoveAllByEndpointId(new Endpoint() { EndpointId = endpointId });
        }

        public override void RemoveAll(string endpointId, UniqueId contextId)
        {
            _internalCache.RemoveAll(endpointId, contextId);

            ApiHelper helper = new ApiHelper(_apiBaseUrl);

            helper.RemoveAll(new Context()
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
            if (cacheServiceAddressElement.Attributes["url"] != null)
            {
                cacheServiceAddress = cacheServiceAddressElement.Attributes["url"].Value;
            }
            else
            {
                throw new ConfigurationErrorsException("<cacheServiceAddress> is expected to contain a 'url' attribute.");
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
            _apiBaseUrl = cacheServiceAddress;
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
