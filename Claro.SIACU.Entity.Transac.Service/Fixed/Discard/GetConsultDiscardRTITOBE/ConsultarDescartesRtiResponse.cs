using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ConsultarDescartesRtiResponse
    {
        [DataMember]
        public ResponseAudit responseAudit { get; set; }
        [DataMember]
        public ResponseData responseData { get; set; }
    }
}
