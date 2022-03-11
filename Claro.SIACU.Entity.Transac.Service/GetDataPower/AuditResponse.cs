using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.GetDataPower
{
    [DataContract(Name = "AuditResponse")]
    public class AuditResponse
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaccion {get; set;}
        
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }
        
        [DataMember(Name = "mensajeRespuesta")]
        public string mensajeRespuesta { get; set; } 

    }
}
