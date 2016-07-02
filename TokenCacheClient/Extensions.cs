using System.Web;

namespace TokenCacheClient
{
    public static class Extensions
    {
        public static string GetRequestPath(this HttpRequest request)
        {
            if (request.ApplicationPath.Length == 1)
                return request.Path;

            return request.Path.Substring(request.ApplicationPath.Length);
        }

    }
}