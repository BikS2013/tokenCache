using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.Replay;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelper
    {
        public bool AddOrUpdate(ReplayCacheEntry request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).AddOrUpdate(rqst);
            return response.Payload;
        }

        public ReplayCacheEntry Get(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).Get(rqst);
            return response.Payload;
        }

        public bool Contains(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).Contains(rqst);
            return response.Payload;
        }

        public bool Remove(string request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).Remove(rqst);
            return response.Payload;
        }

        internal bool Purge()
        {
            var response = new HttpClientHelper(_apiBaseUrl).Purge();
            return response.Payload;
        }
    }
}
