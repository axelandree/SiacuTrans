using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    [DataContract]
    public class ConsultDiscardRTIRequestGrupo : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ConsultDiscardRTIMessageRequestGrupo MessageRequest { get; set; }  
    }
}
