using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice
{
    [DataContract(Name = "ListInvoiceRequestTransactions")]
   public class ListInvoiceRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCODCLIENTE { get; set; }
        [DataMember]
        public string strIdSession { get; set; }
        [DataMember]
        public string strTransaction { get; set; }
        
    }
}
