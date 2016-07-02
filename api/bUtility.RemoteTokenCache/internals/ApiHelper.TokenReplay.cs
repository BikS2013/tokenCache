using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.Replay;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelper
    {
        public bool AddOrUpdate(ReplayCacheEntry request)
        {
            Request<ReplayCacheEntry> rqst = new Request<ReplayCacheEntry>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).AddOrUpdate(rqst);

            return response.Payload;
        }

        public ReplayCacheEntry Get(string request)
        {
            Request<string> rqst = new Request<string>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<ReplayCacheEntry> response = new HttpClientHelper(_apiBaseUrl).Get(rqst);

            return response.Payload;
        }

        public bool Contains(string request)
        {
            Request<string> rqst = new Request<string>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).Contains(rqst);

            return response.Payload;
        }

        public bool Remove(string request)
        {
            Request<string> rqst = new Request<string>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).Remove(rqst);

            return response.Payload;
        }

        internal bool Purge()
        {
            Response<bool> response = new HttpClientHelper(_apiBaseUrl).Purge();

            return response.Payload;
        }
    }
}
