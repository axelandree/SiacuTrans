using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class ConsultDiscardRTIMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultDiscardRTIHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultDiscardRTIBodyRequest Body { get; set; }
    }
}
