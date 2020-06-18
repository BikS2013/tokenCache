using bUtility.TokenCache;
using bUtility.TokenCache.Types.Replay;
using System;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Xml;


namespace bUtility.RemoteTokenCache
{
    public class DistributedTokenReplayCacheAsync : TokenReplayCache, ICustomIdentityConfiguration
    {
        private string _apiBaseUrl = null;
        private DefaultTokenReplayCache _internalCache = null;
        static SecurityTokenSerializer _securityTokenSerializer = null;

        TimeSpan _purgeInterval;
        DateTime _nextPurgeTime = DateTime.UtcNow;


        static DistributedTokenReplayCacheAsync()
        {
            _securityTokenSerializer = new SecurityTokenSerializer(FederatedAuthentication.FederationConfiguration.IdentityConfiguration);
        }
        public DistributedTokenReplayCacheAsync()
        {
        }
        public override void AddOrUpdate(string key, SecurityToken securityToken, DateTime expirationTime)
        {
            Purge();

            _internalCache.AddOrUpdate(key, securityToken, expirationTime);

            ApiHelperAsync helper = new ApiHelperAsync(_apiBaseUrl);

            helper.AddOrUpdate(new ReplayCacheEntry()
            {
                Key = key,
                ExpirationTime = expirationTime,
                SecurityToken = securityToken.SerializeSecurityToken(_securityTokenSerializer)
            });
        }

        public override bool Contains(string key)
        {
            Purge();

            if (!_internalCache.Contains(key))
            {
                ApiHelperAsync helper = new ApiHelperAsync(_apiBaseUrl);

                var token = helper.Get(key);
                if (token != null && token.SecurityToken != null)
                {
                    _internalCache.AddOrUpdate(token.Key, token.SecurityToken.DeserializeSecurityToken(_securityTokenSerializer), token.ExpirationTime);
                    return true;
                }
            }
            return false;
        }

        public override SecurityToken Get(string key)
        {
            Purge();

            var localToken = _internalCache.Get(key);
            if (localToken == null)
            {
                ApiHelperAsync helper = new ApiHelperAsync(_apiBaseUrl);

                var token = helper.Get(key);
                if (token != null && token.SecurityToken != null)
                {
                    _internalCache.AddOrUpdate(token.Key, token.SecurityToken.DeserializeSecurityToken(_securityTokenSerializer), token.ExpirationTime);
                    return _internalCache.Get(key);
                }
                return null;
            }
            else
                return localToken;
        }

        public override void Remove(string key)
        {
            Purge();

            _internalCache.Remove(key);

            ApiHelperAsync helper = new ApiHelperAsync(_apiBaseUrl);

            helper.Remove(key);

        }

        void Purge()
        {
            if (_purgeInterval == TimeSpan.Zero)
                return;

            DateTime currentTime = DateTime.UtcNow;
            if (currentTime < _nextPurgeTime)
            {
                return;
            }

            _nextPurgeTime = currentTime.Add(_purgeInterval);

            ApiHelperAsync helper = new ApiHelperAsync(_apiBaseUrl);

            helper.Purge();
        }

        void ICustomIdentityConfiguration.LoadCustomConfiguration(System.Xml.XmlNodeList nodeList)
        {
            // Retrieve the endpoint address of the centralized session security token cache service running in the web farm
            XmlElement cacheServiceAddressElement = nodeList.GetFirst("No child config element found under <tokenReplayCache>.");
            if (cacheServiceAddressElement?.LocalName != "distributedReplayCacheConfiguration")
            {
                throw new ConfigurationErrorsException("First child config element under <tokenReplayCache> is expected to be <distributedReplayCacheConfiguration>.");
            }

            string cacheServiceAddress = cacheServiceAddressElement.GetStringAttribute("url");
            int maxCacheSize = cacheServiceAddressElement.GetIntAttribute("maxCacheSize");
            int purgeInterval = cacheServiceAddressElement.GetIntAttribute("purgeInterval");

            // Initialize the proxy to the WebFarmSessionSecurityTokenCacheService

            this.Initialize(cacheServiceAddress, maxCacheSize, purgeInterval);
        }

        private void Initialize(string cacheServiceAddress, int maxCacheSize, int purgeIntervalMinutes)
        {
            _apiBaseUrl = cacheServiceAddress;

            _purgeInterval = TimeSpan.FromMinutes(purgeIntervalMinutes);

            _internalCache = new DefaultTokenReplayCache(maxCacheSize, _purgeInterval != TimeSpan.Zero ? _purgeInterval : DefaultTokenReplayCache.DefaultTokenReplayCachePurgeInterval);
        }
    }
}
