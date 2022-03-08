using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServicesDTH
{
    [DataContract(Name = "ServicesDTHResponse")]
    public class ServicesDTHResponse
    {
        [DataMember]
        public int IntResult { get; set; }
        [DataMember]
        public string StrMessageError { get; set; }
        [DataMember]
        public List<ServicesDTH> ListServicesDTH { get; set; }
    }
}
