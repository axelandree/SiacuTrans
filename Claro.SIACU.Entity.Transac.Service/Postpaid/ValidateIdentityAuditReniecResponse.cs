using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityAuditReniecResponse")]
    public class ValidateIdentityAuditReniecResponse
    {
        [DataMember]
        public string autenticacionMensaje { get; set; }
        [DataMember]
        public string codigoAutorizador { get; set; }
        [DataMember]
        public string codigoIdentificacionDispositivo { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string fechaProceso { get; set; }
        [DataMember]
        public string horaProceso { get; set; }
        [DataMember]
        public string latitudLocalizacion { get; set; }
        [DataMember]
        public string longitudLocalizacion { get; set; }
        [DataMember]
        public string marcaDispositivo { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
        [DataMember]
        public string modeloDispositivo { get; set; }
        [DataMember]
        public string numeroIdentificadorCliente { get; set; }
        [DataMember]
        public string numeroProceso { get; set; }
        [DataMember]
        public string numeroTransaccion { get; set; }
        [DataMember]
        public string origenRespuesta { get; set; }
        [DataMember]
        public string tipoIdentificacionDispositivo { get; set; }
        [DataMember]
        public string tipoIdentificadorCliente { get; set; }
        [DataMember]
        public string tipoRespuesta { get; set; }
    }
}
