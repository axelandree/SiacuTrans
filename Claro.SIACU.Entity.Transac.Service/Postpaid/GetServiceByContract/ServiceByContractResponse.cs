using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServiceByContract
{
    public class ServiceByContractResponse
    {
        [DataMember]
        public string StrDesPlan { get; set; }
        [DataMember]
        public string StrResult { get; set; }
        [DataMember]
        public string StrMessage { get; set; }
        [DataMember]
        public List<ServiceByContract> ListServiceByContract { get; set; }
    }
}
