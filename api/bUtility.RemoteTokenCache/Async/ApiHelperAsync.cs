using bUtility.TokenCache.Types;
using System;
using System.Net.Http;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelperAsync
    {
        private readonly HttpClient _httpClient;

        public ApiHelperAsync(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
