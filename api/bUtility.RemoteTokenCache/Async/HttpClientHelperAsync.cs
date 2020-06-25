using bUtility.TokenCache.Types;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelperAsync
    {
        readonly HttpClient _httpClient;

        public HttpClientHelperAsync(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        internal async Task<Rs> ExecuteAsync<Rq, Rs>(Rq data, string actionUrl)
        {
            HttpResponseMessage response = null;
            try
            {
                var url = _httpClient.BaseAddress.ToString() + actionUrl;
                using (var request = new HttpRequestMessage(HttpMethod.Post, url))
                {
                    request.Content = new StringContent(JsonConvert.SerializeObject(data),
                        Encoding.UTF8, "application/json");

                    var token = GetBootstrapContextToken();
                    if (token != null)
                    {
                        var httpFriendlyToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
                        request.Headers.Authorization = new AuthenticationHeaderValue("SAML", httpFriendlyToken);
                    }

                    response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                    if (response?.StatusCode != HttpStatusCode.OK)
                    {
                        throw new TokenCacheException($"unexpected result in RemoteTokenCache HttpClientHelperAsync.ExecuteAsync. " +
                            $"Action url: {actionUrl}, " +
                            $"Status code: {response.StatusCode}, Reason phrase: {response.ReasonPhrase}");
                    }

                    string contentText = default(string);
                    try
                    {
                        contentText = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<Rs>(contentText);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"error Reading response. Content text: [{contentText}]", ex);
                    }
                }
            }
            catch
            {
                throw;            
            }
            finally
            {
                if (response != null)
                {
                    response.Dispose();
                }
            }
        }

        private string GetBootstrapContextToken()
        {
            var identity = ClaimsPrincipal.Current.Identities.FirstOrDefault();
            if (identity != null && (identity.BootstrapContext as BootstrapContext) != null)
            {
                var context = identity.BootstrapContext as BootstrapContext;
                var token = context.SecurityToken;
                if (context.Token != null)
                {
                    return context.Token;
                }

                StringBuilder output = new StringBuilder(128);
                using (var stringWriter = new StringWriter(output))
                {
                    using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                    {
                        context.SecurityTokenHandler.WriteToken(xmlTextWriter, token);
                    }
                }
                return output.ToString();
            }
            return null;
        }

        internal static HttpClient CreateHttpClient(
            string baseAddress,
            int servicePointConnectionLimit,
            int httpClientTimeoutMsecs)
        {
            HttpClient result;

            if (!string.IsNullOrWhiteSpace(baseAddress))
            {
                Uri uriBaseAddress = new Uri(baseAddress);
                ServicePoint sp = ServicePointManager.FindServicePoint(uriBaseAddress);

                sp.Expect100Continue = false;
                sp.UseNagleAlgorithm = false;

                sp.ConnectionLimit = servicePointConnectionLimit;

                result = new HttpClient
                {
                    Timeout = TimeSpan.FromMilliseconds(httpClientTimeoutMsecs),
                    BaseAddress = uriBaseAddress
                };
            }
            else
            {
                result = new HttpClient
                {
                    Timeout = TimeSpan.FromMilliseconds(httpClientTimeoutMsecs)
                };
            }

            return result;
        }
    }
}
