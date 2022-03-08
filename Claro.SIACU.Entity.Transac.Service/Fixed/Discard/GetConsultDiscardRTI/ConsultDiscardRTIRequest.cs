using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    [DataContract]
    public class ConsultDiscardRTIRequest : Tools.Entity.Request
    {
          [DataMember(Name = "MessageRequest")]
          public ConsultDiscardRTIMessageRequest MessageRequest { get; set; }     
    }
}
