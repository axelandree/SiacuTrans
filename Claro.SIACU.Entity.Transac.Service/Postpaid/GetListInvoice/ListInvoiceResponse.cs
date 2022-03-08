using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice
{
    [DataContract(Name = "ListInvoiceResponseTransactions")]
    public class ListInvoiceResponse
    {
        [DataMember]
        public List<Invoice> GetListInvoice { get; set; }
    }
}
