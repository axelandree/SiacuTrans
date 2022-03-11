using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBiometricConfiguration
{
    [DataContract(Name = "BiometricConfigurationResponse")]
    public class BiometricConfigurationResponse
    {
        [DataMember]
        public ValidateIdentityStatusResponse status { get; set; }
        [DataMember]
        public List<BiometricDataConfiguration> data { get; set; }
        [DataMember]
        public List<ValidateIdentityOptionalList> listaOpcional { get; set; }
    }
}
