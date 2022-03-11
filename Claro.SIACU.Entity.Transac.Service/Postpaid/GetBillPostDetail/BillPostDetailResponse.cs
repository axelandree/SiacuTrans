using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail
{
    [DataContract(Name = "BillPostDetailResponseTransactions")]
    public class BillPostDetailResponse
    {
        [DataMember]
        public List<Claro.SIACU.Entity.Transac.Service.Common.CallDetailGeneric> GetBillPostDetail { get; set; }
        
    }
}
