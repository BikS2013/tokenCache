using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.Replay;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelperAsync
    {
        public bool AddOrUpdate(ReplayCacheEntry request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).AddOrUpdate(rqst);
            return response.Payload;
        }

        public ReplayCacheEntry Get(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).Get(rqst);
            return response.Payload;
        }

        public bool Contains(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).Contains(rqst);
            return response.Payload;
        }

        public bool Remove(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).Remove(rqst);
            return response.Payload;
        }

        internal bool Purge()
        {
            var response = new HttpClientHelperAsync(_httpClient).Purge();
            return response.Payload;
        }
    }
}
