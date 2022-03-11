using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
//INI - RF-04 - RF-05 Evalenzs
    [DataContract(Name = "ConsultarPaqDisponibles Response")]
    public class ConsultarPaqDisponiblesResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ConsultarPaqDisponiblesMessageResponse MessageResponse { get; set; }
    }
}
