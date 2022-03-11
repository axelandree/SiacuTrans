using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityBiometryDataRequest")]
    public class ValidateIdentityBiometryDataRequest
    {
        [DataMember]
        public string tipoIdentificacionInstitucion { get; set; }
        [DataMember]
        public string numeroIdentificadorInstitucion { get; set; }
        [DataMember]
        public string tipoDocumento { get; set; }
        [DataMember]
        public string numeroDocumento { get; set; }
        [DataMember]
        public string tipoVerificacion { get; set; }
        [DataMember]
        public List<ValidateIdentityFingerprintData> huellasBiometrica { get; set; }
    }
}
