using System.Web.Http;
using bUtility.TokenCache.Types;
using bUtility.TokenCache.Types.SessionSecurity;
using System.IdentityModel.Tokens;
using System.Linq;
using bUtility.TokenCache.Interfaces;

namespace bUtility.TokenCache.Controllers
{
    public class TokenCacheController : ApiController
    {
        readonly IDistributedSessionSecurityTokenCache Service = null;
        public TokenCacheController(IDistributedSessionSecurityTokenCache service)
        {
            Service = service;
        }

        [ActionName("addOrUpdate")]
        [HttpPost]
        public Response<bool> AddOrUpdate(Request<SessionCacheEntry> request)
        {
            var data = Service.AddOrUpdate(request.Payload);
            return new Response<bool> { Payload = data };
        }

        [ActionName("getAll")]
        [HttpPost]
        public Response<SessionSecurityToken[]> GetAll(Request<Context> request)
        {
            var data = Service.GetAll(request.Payload);
            return new Response<SessionSecurityToken[]> { Payload = data.ToArray() };
        }

        [ActionName("get")]
        [HttpPost]
        public Response<SessionSecurityToken> Get(Request<SessionCacheKey> request)
        {
            var data = Service.Get(request.Payload);
            return new Response<SessionSecurityToken> { Payload = data };
        }

        [ActionName("remove")]
        [HttpPost]
        public Response<bool> Remove(Request<SessionCacheKey> request)
        {
            var data = Service.Remove(request.Payload);
            return new Response<bool> { Payload = data };
        }

        [ActionName("removeAllByEndpointId")]
        [HttpPost]
        public Response<bool> RemoveAllByEndpointId(Request<Endpoint> request)
        {
            var data = Service.RemoveAllByEndpointId(request.Payload);
            return new Response<bool> { Payload = data };
        }

        [ActionName("removeAll")]
        [HttpPost]
        public Response<bool> RemoveAll(Request<Context> request)
        {
            var data = Service.RemoveAll(request.Payload);
            return new Response<bool> { Payload = data };
        }
    }
}