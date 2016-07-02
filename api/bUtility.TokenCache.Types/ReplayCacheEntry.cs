using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types.Replay
{
    [DataContract]
    public class ReplayCacheEntry
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "securityToken")]
        public string SecurityToken { get; set; }

        [DataMember(Name = "expirationTime")]
        public DateTime ExpirationTime { get; set; }
    }
}
