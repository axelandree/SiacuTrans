using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityFingerprintData")]
    public class ValidateIdentityFingerprintData
    {
        [DataMember]
        public string idHuella { get; set; }
        [DataMember]
        public string calidadImagen { get; set; }
        [DataMember]
        public string huellaImagen { get; set; }
        [DataMember]
        public string huellaTemplate { get; set; }
        [DataMember]
        public string formatoBiometrico { get; set; }
    }
}
