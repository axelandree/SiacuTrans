using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI
{
    [DataContract(Name = "ListCallDetailPDIResponseTransactions")]
    public class ListCallDetailPDIResponse
    {
        [DataMember]
        public List<ListCallDetail_PDI> GetListCallDetailPDI { get; set; }
    }
}
