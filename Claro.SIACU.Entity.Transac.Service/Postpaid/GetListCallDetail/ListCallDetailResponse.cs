using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail
{
    [DataContract(Name = "ListCallDetailResponseTransactions")]
    public class ListCallDetailResponse
    {
        [DataMember]
        public List<ListCallDetail> GetListCallDetail { get; set; }
    }
}
