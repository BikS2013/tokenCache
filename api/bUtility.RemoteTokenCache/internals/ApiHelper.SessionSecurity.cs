﻿using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;

namespace bUtility.RemoteTokenCache
{
    internal partial class ApiHelper
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
            var response = new HttpClientHelper(_apiBaseUrl).AddOrUpdate(rqst);
            return response.Payload;
        }

        public SessionSecurityToken[] GetAll(Context request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).GetAll(rqst);
            return response.Payload;
        }

        public SessionSecurityToken Get(SessionCacheKey request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).Get(rqst);
            return response.Payload;
        }

        public bool Remove(SessionCacheKey request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).Remove(rqst);
            return response.Payload;
        }

        public bool RemoveAllByEndpointId(Endpoint request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).RemoveAllByEndpointId(rqst);
            return response.Payload;
        }

        public bool RemoveAll(Context request)
        {
            var rqst = GetRequest(request);
            var response = new HttpClientHelper(_apiBaseUrl).RemoveAll(rqst);
            return response.Payload;
        }
    }
}
