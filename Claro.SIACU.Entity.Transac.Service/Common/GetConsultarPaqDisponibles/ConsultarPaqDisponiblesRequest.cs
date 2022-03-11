using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarPaqDisponibles Request")]
    public class ConsultarPaqDisponiblesRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public ConsultarPaqDisponiblesMessageRequest MessageRequest { get; set; }      
    }
}
