using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetCallDetailNBDB1BSCS
{
    [DataContract(Name = "CallDetailNBDB1BSCSRequestPostPaid")]
    public class CallDetailNBDB1BSCSRequest : Claro.Entity.Request
    {
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string StrStartDate { get; set; }
        [DataMember]
        public string StrEndDate { get; set; }
        [DataMember]
        public string StrYesterday { get; set; }
        [DataMember]
        public string StrToday { get; set; }
        [DataMember]
        public string Security { get; set; }
    }
}
