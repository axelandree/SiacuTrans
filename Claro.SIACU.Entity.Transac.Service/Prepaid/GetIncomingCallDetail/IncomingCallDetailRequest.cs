using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetIncomingCallDetail
{
    [DataContract(Name = "IncomingCallDetailRequestPrepaid")]
    public class IncomingCallDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string MSISDN { get; set; }
        [DataMember]
        public string StrStartDate { get; set; }
        [DataMember]
        public string StrEndDate { get; set; }

    }
}
