using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceBSCS
{
    [DataContract(Name = "ServiceBSCSRequest")]
    public class ServiceBSCSRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrUser { get; set; }
        [DataMember]
        public string StrSystem { get; set; }
        [DataMember]
        public string StrCodServ { get; set; }
    }
}
