using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.Text;
using System.Xml;

namespace bUtility.TokenCache
{
    public class SecurityTokenSerializer
    {
        private readonly IdentityConfiguration _identityConfiguration = null;
        public SecurityTokenSerializer(IdentityConfiguration identityConfiguration)
        {
            _identityConfiguration = identityConfiguration;
        }
        public string SerializeToken(SecurityToken token)
        {
            var handlers = _identityConfiguration.SecurityTokenHandlers;

            StringBuilder output = new StringBuilder(128);
            handlers.WriteToken(new XmlTextWriter(new StringWriter(output)), token);
            return output.ToString();
        }

        public SecurityToken DeserializeToken(string token)
        {
            var handlers = _identityConfiguration.SecurityTokenHandlers;

            using (var textReader = new StringReader(token))
            using (var xmlReader = new XmlTextReader(textReader))
            {
                var stoken = handlers.ReadToken(xmlReader);
                return stoken;
            }
        }
    }
}
