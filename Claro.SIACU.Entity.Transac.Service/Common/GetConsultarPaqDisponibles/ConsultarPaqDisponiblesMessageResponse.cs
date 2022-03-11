using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarPaqDisponibles MessageResponse")]
    public class ConsultarPaqDisponiblesMessageResponse
    {
        [DataMember(Name = "Header")]
        public ConsultarPaqDisponiblesHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultarPaqDisponiblesBodyResponse Body { get; set; }
    }
}
