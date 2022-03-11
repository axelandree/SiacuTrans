using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
     [DataContract]
    public class ConsultSNRequest : Tools.Entity.Request
    {
         [DataMember(Name = "MessageRequest")]
         public ConsultSNMessageRequest MessageRequest { get; set; } 
    }
}
