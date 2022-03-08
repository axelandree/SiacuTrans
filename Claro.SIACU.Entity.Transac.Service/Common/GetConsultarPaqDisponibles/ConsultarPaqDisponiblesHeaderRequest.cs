using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
//INI - RF-04 - RF-05 Evalenzs
namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
    [DataContract(Name = "ConsultarPaqDisponibles HeaderRequest")]
    public class ConsultarPaqDisponiblesHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public  Claro.SIACU.Entity.Transac.Service.Common.GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
