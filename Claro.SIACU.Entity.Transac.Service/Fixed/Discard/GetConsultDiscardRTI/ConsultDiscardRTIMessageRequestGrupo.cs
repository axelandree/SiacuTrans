using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class ConsultDiscardRTIMessageRequestGrupo
    {
        [DataMember(Name = "Header")]
        public ConsultDiscardRTIHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultDiscardRTIBodyRequestGrupo Body { get; set; }
    }
}
