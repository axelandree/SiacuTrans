using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class ConsultDiscardRTIMessageResponse
    {
        [DataMember(Name = "Header")]
        public ConsultDiscardRTIHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultDiscardRTIBodyResponse Body { get; set; }
    }
}
