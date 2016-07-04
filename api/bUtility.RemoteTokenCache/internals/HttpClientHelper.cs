using bUtility.TokenCache.Types;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace bUtility.RemoteTokenCache
{
    internal partial class HttpClientHelper
    {
        readonly string _webApiBaseUrl = null;

        public HttpClientHelper(string webApiBaseUrl)
        {
            _webApiBaseUrl = webApiBaseUrl;
            if (!_webApiBaseUrl.EndsWith("/")) {
                _webApiBaseUrl = _webApiBaseUrl + "/";
            }
        }
        static HttpClientHelper()
        {
#warning remove from production
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }


        internal Rs Execute<Rq, Rs>(Rq data, string actionUrl)
        {
            HttpResponseMessage response = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_webApiBaseUrl);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var token = GetBootstrapContextToken();
                    if (token != null)
                    {
                        var httpFriendlyToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SAML", httpFriendlyToken);
                    }

                    var formatter = new JsonMediaTypeFormatter
                    {
                        SerializerSettings = new JsonSerializerSettings
                        {
                            Formatting = Newtonsoft.Json.Formatting.Indented
                        }
                    };

                    response = client.PostAsync<Rq>(actionUrl, data, formatter).Result;

                    if ( response?.StatusCode != HttpStatusCode.OK)
                    {
                        throw new TokenCacheException($"unexpected result in RemoteTokenCache HttpClient.Execute. " + 
                            $"Action url: {actionUrl}, "+
                            $"Status code: {response.StatusCode}, Reason phrase: {response.ReasonPhrase}");
                    }
                    try
                    {
                        var result = response.Content.ReadAsAsync<Rs>(new[] { formatter }).Result;
                        return result;
                    }
                    catch(Exception ex)
                    {
                        var result = response.Content.ReadAsAsync<string>().Result;
                        throw new Exception( $"error Reading response. text: {result}", ex);
                    }
                }
            }
            catch (TokenCacheException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (response == null)
                {
                    throw new Exception("error executing HttpClientHelper", ex);
                }
                else
                {
                    string content = null;
                    try
                    {
                        content = response.Content.ReadAsStringAsync().Result;
                    }
                    finally
                    {
                        if (content != null)
                            throw new Exception($"Response = {response}{System.Environment.NewLine}Content = {content}", ex);
                        else
                            throw new Exception($"Response = {response}", ex);
                    }
                }
                throw;
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
                context.SecurityTokenHandler.WriteToken(new XmlTextWriter(new StringWriter(output)), token);
                return output.ToString();
            }
            return null;
        }

    }
}
