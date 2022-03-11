using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarPaqDisponiblesMessageRequest")]
    public class ConsultarPaqDisponiblesMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultarPaqDisponiblesHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultarPaqDisponiblesBodyRequest Body { get; set; }
    }          
}
