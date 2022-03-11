using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
      [DataContract(Name = "operatorProvisioningProductResponse")]
    public class OperatorProvisioningProductResponse
    {
        [DataMember(Name = "result")]
        public Result result { get; set; }

        [DataMember(Name = "hubTransID")]
        public string hubTransID { get; set; }

        [DataMember(Name = "operatorUserID")]
        public string operatorUserID { get; set; }

        [DataMember(Name = "childuserId")]
        public string childuserId { get; set; }

        [DataMember(Name = "providerUserID")]
        public string providerUserID { get; set; }

        [DataMember(Name = "extensionInfo")]
        public List<ExtensionInfo> extensionInfo { get; set; }
    }
}
