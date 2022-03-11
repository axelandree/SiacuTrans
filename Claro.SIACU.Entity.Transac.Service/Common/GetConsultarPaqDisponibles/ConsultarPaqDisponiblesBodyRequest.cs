using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultarPaqDisponibles 
{
    //INI - RF-02Evalenzs
    [DataContract(Name = "ConsultarPaqDisponiblesBodyRequest")]
    public class ConsultarPaqDisponiblesBodyRequest
    {
       [DataMember]
        public string idCategoria { get; set; }

        [DataMember]
        public string idContrato { get; set; }

        [DataMember]
        public string codigoCategoria { get; set; }

        [DataMember]
        public string prepagoCode { get; set; }
        [DataMember]
        public string tmCode { get; set; }
 
    }
    public class PCRFPaquetesAdicConsultarRequest
    {

        
    }

}
