using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace bUtility.TokenCache.Types
{
    public enum ErrorSeverity { Warning, Error, Info }
    public enum ErrorCategory { Business, Communication, Technical, Security }

    /// <summary>
    /// A message for a service response.
    /// </summary>
    [DataContract]
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "desc")]
        public string Description { get; set; }
        [DataMember(Name = "sev")]
        public ErrorSeverity Severity { get; set; }
        [DataMember(Name = "cat")]
        public ErrorCategory Category { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} ({3})", this.Severity, this.Category, this.Code, this.Description);
        }
    }

    /// <summary>
    /// Wrap a response object together with any possible messages.
    /// </summary>
    [DataContract]
    public class Response<T> 
    {
        [DataMember(Name = "payload")]
        public T Payload { get; set; }

        public Response() { }

        public Response(T payload)
        {
            this.Payload = payload;
        }

        [DataMember(Name = "exception")]
        public ResponseMessage Exception { get; set; }

    }
}
