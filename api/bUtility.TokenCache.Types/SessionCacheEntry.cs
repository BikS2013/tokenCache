using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types.SessionSecurity
{
    [DataContract]
    public class SessionCacheEntry
    {
        [DataMember(Name = "endpointId")]
        public string EndpointId { get; set; }

        [DataMember(Name = "contextId")]
        public string ContextId { get; set; }

        [DataMember(Name = "keyGeneration")]
        public string KeyGeneration { get; set; }

        [DataMember(Name = "sessionSecurityTokenValue")]
        public SessionSecurityToken SessionSecurityTokenValue { get; set; }

        [DataMember(Name = "sessionSecurityTokenID")]
        public string SessionSecurityTokenID { get; set; }

        [DataMember(Name = "expiryTime")]
        public DateTime ExpiryTime { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }
    }
}
