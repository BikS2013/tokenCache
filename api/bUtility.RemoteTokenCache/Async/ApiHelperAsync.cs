using bUtility.TokenCache.Types;
using System;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelperAsync
    {
        private string _apiBaseUrl = null;
        public ApiHelperAsync(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }
        private RequestHeader ApiHeader
        {
            get
            {
                return new RequestHeader()
                {
                    Application = DistributedSessionSecurityTokenCacheAsync.ApplicationId,
                    ID = Guid.NewGuid().ToString(),
                };
            }
        }


    }
}
