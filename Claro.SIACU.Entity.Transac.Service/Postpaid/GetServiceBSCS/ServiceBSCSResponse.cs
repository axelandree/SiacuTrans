using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceBSCS
{
    [DataContract(Name = "ServiceBSCSResponse")]
    public class ServiceBSCSResponse
    {
        [DataMember]
        public string StrDesServ { get; set; }
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMsg { get; set; }
        [DataMember]
        public List<ServiceBSCS> ListServiceBSCS { get; set; }
    }
}
