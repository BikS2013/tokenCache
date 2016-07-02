using bUtility.TokenCache.Types;
using System;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelper
    {
        private string _apiBaseUrl = null;
        public ApiHelper(string apiBaseUrl)
        {
            _apiBaseUrl = apiBaseUrl;
        }
        private RequestHeader ApiHeader
        {
            get
            {
                return new RequestHeader()
                {
                    Application = DistributedSessionSecurityTokenCache.ApplicationId,
                    ID = Guid.NewGuid().ToString(),
                };
            }
        }


    }
}
