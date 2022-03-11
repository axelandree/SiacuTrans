using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityAuditReniecRequest")]
    public class ValidateIdentityAuditReniecRequest
    {
        [DataMember]
        public string autenticacionMensaje { get; set; }
        [DataMember]
        public string codigoIdentificacionDispositivo { get; set; }
        [DataMember]
        public string latitudLocalizacion { get; set; }
        [DataMember]
        public string longitudLocalizacion { get; set; }
        [DataMember]
        public string marcaDispositivo { get; set; }
        [DataMember]
        public string modeloDispositivo { get; set; }
        [DataMember]
        public string numeroIdentificadorCliente { get; set; }
        [DataMember]
        public string numeroOrden { get; set; }
        [DataMember]
        public string numeroTransaccion { get; set; }
        [DataMember]
        public string tipoDispositivo { get; set; }
        [DataMember]
        public string tipoEstacion { get; set; }
        [DataMember]
        public string tipoIdentificacionDispositivo { get; set; }
        [DataMember]
        public string tipoIdentificadorCliente { get; set; }
        [DataMember]
        public string versionAplicativo { get; set; }
    }
}
