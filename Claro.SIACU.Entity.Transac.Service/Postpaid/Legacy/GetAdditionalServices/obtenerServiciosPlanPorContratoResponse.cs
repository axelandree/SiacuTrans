using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetAdditionalServices
{
    [DataContract]
    public class obtenerServiciosPlanPorContratoResponse
    {
        [DataMember(Name = "responseAudit")]
        public responseAudit responseAudit { get; set; }
        [DataMember(Name = "responseData")]
        public responseData responseData { get; set; }
    }
}
