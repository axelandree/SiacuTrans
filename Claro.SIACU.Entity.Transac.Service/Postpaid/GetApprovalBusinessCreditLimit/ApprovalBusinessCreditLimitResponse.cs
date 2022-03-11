using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit
{
    [DataContract(Name = "ApprovalBusinessCreditLimitResponse")]
    public class ApprovalBusinessCreditLimitResponse
    {
        [DataMember]
        public decimal NewCharge { get; set; }
        [DataMember]
        public decimal MaxCharge { get; set; }
        [DataMember]
        public Int64 Error { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public bool result { get; set; }
    }
}
