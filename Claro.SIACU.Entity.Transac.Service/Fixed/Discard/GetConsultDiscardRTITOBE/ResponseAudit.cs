using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.GetConsultDiscardRTITOBE
{
    [DataContract]
    public class ResponseAudit
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
