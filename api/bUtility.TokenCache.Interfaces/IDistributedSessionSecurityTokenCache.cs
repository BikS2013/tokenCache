using bUtility.TokenCache.Types.SessionSecurity;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace bUtility.TokenCache.Interfaces
{
    public interface IDistributedSessionSecurityTokenCache
    {
        bool AddOrUpdate(SessionCacheEntry request);

        IEnumerable<SessionSecurityToken> GetAll(Context request);

        SessionSecurityToken Get(SessionCacheKey request);

        bool Remove(SessionCacheKey request);

        bool RemoveAllByEndpointId(Endpoint request);

        bool RemoveAll(Context request);
    }
}
