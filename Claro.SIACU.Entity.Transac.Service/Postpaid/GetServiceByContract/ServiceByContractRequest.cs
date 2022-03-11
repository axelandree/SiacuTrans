using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract
{
    [DataContract(Name = "ServiceByContractRequest")]
    public class ServiceByContractRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrUser { get; set; }
        [DataMember]
        public string StrSystem { get; set; }
        [DataMember]
        public string StrCoid { get; set; }

    }
}
