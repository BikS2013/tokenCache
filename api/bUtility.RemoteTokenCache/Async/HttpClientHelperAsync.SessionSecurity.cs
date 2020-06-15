using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelperAsync
    {
        internal Response<bool> AddOrUpdate(Request<SessionCacheEntry> request)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<SessionCacheEntry>,
                Response<bool>>(request, "tokenCache/addOrUpdate"));
            return task.Result;
        }

        internal Response<SessionSecurityToken[]> GetAll(Request<Context> request)
        {
            Task<Response<SessionSecurityToken[]>> task = Task.Run(async () => await ExecuteAsync<Request<Context>,
                Response<SessionSecurityToken[]>>(request, "tokenCache/getAll"));
            return task.Result;
        }

        internal Response<SessionSecurityToken> Get(Request<SessionCacheKey> request)
        {
            Task<Response<SessionSecurityToken>> task = Task.Run(async () => await ExecuteAsync<Request<SessionCacheKey>,
                Response<SessionSecurityToken>>(request, "tokenCache/get"));
            return task.Result;
        }

        internal Response<bool> Remove(Request<SessionCacheKey> request)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<SessionCacheKey>,
                Response<bool>>(request, "tokenCache/remove"));
            return task.Result;
        }

        internal Response<bool> RemoveAllByEndpointId(Request<Endpoint> request)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<Endpoint>,
                Response<bool>>(request, "tokenCache/removeAllByEndpointId"));
            return task.Result;
        }

        internal Response<bool> RemoveAll(Request<Context> request)
        {
            Task<Response<bool>> task = Task.Run(async () => await ExecuteAsync<Request<Context>,
                Response<bool>>(request, "tokenCache/removeAll"));
            return task.Result;
        }
    }
}
