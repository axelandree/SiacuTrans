using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    [DataContract]
    public class ConsultClientSNRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ConsultClientSNMessageRequest MessageRequest { get; set; } 
    }
}
