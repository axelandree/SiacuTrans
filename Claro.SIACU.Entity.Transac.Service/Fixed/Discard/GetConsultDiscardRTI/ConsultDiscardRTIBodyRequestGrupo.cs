using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class ConsultDiscardRTIBodyRequestGrupo
    {
        [DataMember(Name = "consultarDescartesRtiPrePostRequest")]
        public consultarDescartesRtiPrePostRequestGrupo consultarDescartesRtiPrePostRequest { get; set; }
    }
}
