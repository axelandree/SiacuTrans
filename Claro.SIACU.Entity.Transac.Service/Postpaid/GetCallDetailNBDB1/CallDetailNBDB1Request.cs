using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetCallDetailNBDB1
{
    [DataContract(Name = "CallDetailNBDB1RequestPostPaid")]
    public class CallDetailNBDB1Request : Claro.Entity.Request
    {
        [DataMember]
        public string ContractID { get; set; }
        [DataMember]
        public string StrStartDate { get; set; }
        [DataMember]
        public string StrEndDate { get; set; }
        [DataMember]
        public string Security { get; set; }
    }
}
