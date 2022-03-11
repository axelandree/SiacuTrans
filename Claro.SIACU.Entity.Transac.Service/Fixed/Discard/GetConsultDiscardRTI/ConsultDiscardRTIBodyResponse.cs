using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    
    public class ConsultDiscardRTIBodyResponse
    {

        [DataMember(Name = "consultarDescartesRtiPrePostResponse")]
        public consultarDescartesRtiPrePostResponse consultarDescartesRtiPrePostResponse { get; set; }
    }
}
