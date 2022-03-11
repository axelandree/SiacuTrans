using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
      [DataContract]
    public class ConsultDiscardRTIResponse
    {
          [DataMember(Name = "MessageResponse")]
          public ConsultDiscardRTIMessageResponse MessageResponse { get; set; }
    }
}
