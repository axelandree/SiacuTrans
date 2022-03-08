using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetServicesDTH
{
    [DataContract(Name = "ServicesDTHRequest")]
    public class ServicesDTHRequest: Claro.Entity.Request
    {
        [DataMember]
        public int intCoId { get; set; }
        [DataMember]
        public string strMsisdn { get; set; }
    }
}
