using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class AuditResponse
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
