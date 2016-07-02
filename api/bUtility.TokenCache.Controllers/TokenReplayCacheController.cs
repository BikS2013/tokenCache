using System.Web.Http;
using bUtility.TokenCache.Types.Replay;
using bUtility.TokenCache.Types;
using bUtility.TokenCache.Interfaces;

namespace bUtility.TokenCache.Controllers
{
    public class TokenReplayCacheController : ApiController
    {
        readonly IDistributedTokenReplayCache Service;
        public TokenReplayCacheController(IDistributedTokenReplayCache service)
        {
            Service = service;
        }

        [ActionName("addOrUpdate")]
        [HttpPost]
        public Response<bool> AddOrUpdate(Request<ReplayCacheEntry> request)
        {
            var data = Service.AddOrUpdate(request.Payload);
            return new Response<bool> { Payload = data };
        }


        [ActionName("get")]
        [HttpPost]
        public Response<ReplayCacheEntry> Get(Request<string> request)
        {
            var data = Service.Get(request.Payload);
            return new Response<ReplayCacheEntry> { Payload = data };
        }

        [ActionName("contains")]
        [HttpPost]
        public Response<bool> Contains(Request<string> request)
        {
            var data = Service.Contains(request.Payload);
            return new Response<bool> { Payload = data };
        }

        [ActionName("remove")]
        [HttpPost]
        public Response<bool> Remove(Request<string> request)
        {
            var data = Service.Remove(request.Payload);
            return new Response<bool> { Payload = data };
        }

        [ActionName("purge")]
        [HttpPost]
        public Response<bool> Purge()
        {
            var data = Service.Purge();
            return new Response<bool> { Payload = data };
        }
    }
}