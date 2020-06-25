using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelperAsync
    {
        Request<T> GetRequest<T>(T request) where T:class
        {
            return new Request<T>
            {
                Header = ApiHeader,
                Payload = request
            };
        }
        public bool AddOrUpdate(SessionCacheEntry request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).AddOrUpdate(rqst);
            return response.Payload;
        }

        public SessionSecurityToken[] GetAll(Context request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).GetAll(rqst);
            return response.Payload;
        }

        public SessionSecurityToken Get(SessionCacheKey request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).Get(rqst);
            return response.Payload;
        }

        public bool Remove(SessionCacheKey request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).Remove(rqst);
            return response.Payload;
        }

        public bool RemoveAllByEndpointId(Endpoint request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).RemoveAllByEndpointId(rqst);
            return response.Payload;
        }

        public bool RemoveAll(Context request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelperAsync(_httpClient).RemoveAll(rqst);
            return response.Payload;
        }
    }
}
