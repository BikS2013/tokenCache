using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.Replay;
using System.Threading.Tasks;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelperAsync
    {
        internal Response<bool> AddOrUpdate(Request<ReplayCacheEntry> rqst)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<ReplayCacheEntry>, 
                Response<bool>>(rqst, "tokenReplayCache/addOrUpdate"));
            return task.Result;
        }

        internal Response<ReplayCacheEntry> Get(Request<string> rqst)
        {
            Task<Response<ReplayCacheEntry>> task = Task.Run(async () => await ExecuteAsync<Request<string>, 
                Response<ReplayCacheEntry>>(rqst, "tokenReplayCache/get"));
            return task.Result;
        }

        internal Response<bool> Contains(Request<string> rqst)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<string>, 
                Response<bool>>(rqst, "tokenReplayCache/contains"));
            return task.Result;
        }

        internal Response<bool> Remove(Request<string> rqst)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<string>, 
                Response<bool>>(rqst, "tokenReplayCache/remove"));
            return task.Result;
        }

        internal Response<bool> Purge()
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<string, 
                Response<bool>>("", "tokenReplayCache/purge"));
            return task.Result;
        }
    }
}
