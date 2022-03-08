using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetApprovalBusinessCreditLimit
{
    [DataContract(Name = "ApprovalBusinessCreditLimitRequest")]
    public class ApprovalBusinessCreditLimitRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Account { get; set; }
        [DataMember]
        public int Contract { get; set; }
        [DataMember]
        public int Service { get; set; }
    }
}
