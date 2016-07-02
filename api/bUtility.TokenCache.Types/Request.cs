using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types
{
    [DataContract]
    public class RequestHeader
    {
        [DataMember(Name = "ID")]
        [Required]
        public string ID { get; set; }

        [DataMember(Name = "application")]
        public string Application { get; set; }

        [DataMember(Name = "logitude")]
        public decimal? Longitude { get; set; }

        [DataMember(Name = "latitude")]
        public decimal? Latitude { get; set; }
    }


    [DataContract]
    public class Request<T> where T : class
    {
        [DataMember(Name = "header")]
        public RequestHeader Header { get; set; }

        [DataMember(Name = "payload")]
        public T Payload { get; set; }

    }
}
