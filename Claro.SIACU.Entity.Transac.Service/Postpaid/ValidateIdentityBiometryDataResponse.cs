using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityBiometryDataResponse")]
    public class ValidateIdentityBiometryDataResponse
    {
        public ValidateIdentityBiometryDataResponse()
        {
            confiabilidadHuella = new List<ValidateIdentityFingerprintReliability>();
        }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string codigoGrupoRestriccion { get; set; }
        [DataMember]
        public string codigoRestriccion { get; set; }
        [DataMember]
        public string descripcionGrupoRestriccion { get; set; }
        [DataMember]
        public string descripcionRestriccion { get; set; }
        [DataMember]
        public string fechaCaducidad { get; set; }
        [DataMember]
        public string fechaEmision { get; set; }
        [DataMember]
        public string fechaNacimiento { get; set; }
        [DataMember]
        public string firma { get; set; }
        [DataMember]
        public string foto { get; set; }
        [DataMember]
        public string idSesion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string dniCaducado { get; set; }
        [DataMember]
        public List<ValidateIdentityFingerprintReliability> confiabilidadHuella { get; set; }
    }
}
