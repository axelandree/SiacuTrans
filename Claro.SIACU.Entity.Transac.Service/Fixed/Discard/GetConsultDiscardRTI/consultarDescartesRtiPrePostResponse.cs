using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTI
{
    public class consultarDescartesRtiPrePostResponse
    {
        [DataMember(Name = "responseAudit")]
        public Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE.ResponseAudit responseAudit { get; set; }

        [DataMember(Name = "responseData")]
        public Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE.ResponseData responseData { get; set; }
    }
}
