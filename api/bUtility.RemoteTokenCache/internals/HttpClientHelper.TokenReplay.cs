using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.Replay;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelper
    {
        internal Response<bool> AddOrUpdate(Request<ReplayCacheEntry> rqst)
        {
            return Execute<Request<ReplayCacheEntry>, Response<bool>>(rqst, "tokenReplayCache/addOrUpdate");
        }

        internal Response<ReplayCacheEntry> Get(Request<string> rqst)
        {
            return Execute<Request<string>, Response<ReplayCacheEntry>>(rqst, "tokenReplayCache/get");
        }

        internal Response<bool> Contains(Request<string> rqst)
        {
            return Execute<Request<string>, Response<bool>>(rqst, "tokenReplayCache/contains");
        }

        internal Response<bool> Remove(Request<string> rqst)
        {
            return Execute<Request<string>, Response<bool>>(rqst, "tokenReplayCache/remove");
        }
        internal Response<bool> Purge()
        {
            return Execute<string, Response<bool>>("", "tokenReplayCache/purge");
        }

    }
}
