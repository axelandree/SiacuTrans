using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList
{
    [DataContract(Name = "RechargeListResponseTransactions")]
    public class RechargeListResponse
    {
        [DataMember]
        public List<RechargeList> GetRechargeList { get; set; }
    }
}
