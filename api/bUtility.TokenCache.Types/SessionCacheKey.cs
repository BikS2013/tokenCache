using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types.SessionSecurity
{
    [DataContract]
    public class SessionCacheKey
    {
        [DataMember(Name = "endpointId")]
        public string EndpointId { get; set; }

        [DataMember(Name = "contextId")]
        public string ContextId { get; set; }

        [DataMember(Name = "keyGeneration")]
        public string KeyGeneration { get; set; }
    }
}
