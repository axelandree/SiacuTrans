using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using OPostBillOutCallDetail = Claro.SIACU.Entity.Transac.Service.Postpaid;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI
{
    [DataContract(Name = "ListInvoice_PDIResponseTransactions")]
    public class ListInvoice_PDIResponse
    {
        [DataMember]
        public List<Invoice_PDI> GetListInvoicePDI { get; set; }
    }
}
