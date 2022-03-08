using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ValidateIdentityFingerprintReliability")]
    public class ValidateIdentityFingerprintReliability
    {
        [DataMember]
        public string idHuella { get; set; }
        [DataMember]
        public string porcentajeConfiabilidad { get; set; }
    }
}
