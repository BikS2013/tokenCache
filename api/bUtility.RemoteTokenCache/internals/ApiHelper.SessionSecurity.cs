using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelper
    {
        public bool AddOrUpdate(SessionCacheEntry request)
        {
            Request<SessionCacheEntry> rqst = new Request<SessionCacheEntry>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).AddOrUpdate(rqst);

            return response.Payload;
        }

        public SessionSecurityToken[] GetAll(Context request)
        {
            Request<Context> rqst = new Request<Context>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<SessionSecurityToken[]> response = new HttpClientHelper(_apiBaseUrl).GetAll(rqst);

            return response.Payload;
        }

        public SessionSecurityToken Get(SessionCacheKey request)
        {
            Request<SessionCacheKey> rqst = new Request<SessionCacheKey>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<SessionSecurityToken> response = new HttpClientHelper(_apiBaseUrl).Get(rqst);

            return response.Payload;
        }

        public bool Remove(SessionCacheKey request)
        {
            Request<SessionCacheKey> rqst = new Request<SessionCacheKey>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).Remove(rqst);

            return response.Payload;
        }

        public bool RemoveAllByEndpointId(Endpoint request)
        {
            Request<Endpoint> rqst = new Request<Endpoint>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).RemoveAllByEndpointId(rqst);

            return response.Payload;
        }

        public bool RemoveAll(Context request)
        {
            Request<Context> rqst = new Request<Context>();
            rqst.Header = ApiHeader;

            rqst.Payload = request;

            Response<bool> response = new HttpClientHelper(_apiBaseUrl).RemoveAll(rqst);

            return response.Payload;
        }
    }
}
