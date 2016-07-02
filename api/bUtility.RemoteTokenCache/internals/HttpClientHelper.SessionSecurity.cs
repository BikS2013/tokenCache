using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelper
    {
        internal Response<bool> AddOrUpdate(Request<SessionCacheEntry> request)
        {
            return Execute<Request<SessionCacheEntry>, Response<bool>>(request, "tokenCache/addOrUpdate");
        }

        internal Response<SessionSecurityToken[]> GetAll(Request<Context> request)
        {
            return Execute<Request<Context>, Response<SessionSecurityToken[]>>(request, "tokenCache/getAll");
        }

        internal Response<SessionSecurityToken> Get(Request<SessionCacheKey> request)
        {
            return Execute<Request<SessionCacheKey>, Response<SessionSecurityToken>>(request, "tokenCache/get");
        }

        internal Response<bool> Remove(Request<SessionCacheKey> request)
        {
            return Execute<Request<SessionCacheKey>, Response<bool>>(request, "tokenCache/remove");
        }

        internal Response<bool> RemoveAllByEndpointId(Request<Endpoint> request)
        {
            return Execute<Request<Endpoint>, Response<bool>>(request, "tokenCache/removeAllByEndpointId");
        }

        internal Response<bool> RemoveAll(Request<Context> request)
        {
            return Execute<Request<Context>, Response<bool>>(request, "tokenCache/removeAll");
        }


    }
}
