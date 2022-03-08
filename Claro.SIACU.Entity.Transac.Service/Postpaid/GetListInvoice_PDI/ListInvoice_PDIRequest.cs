using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListInvoice_PDI
{
    [DataContract(Name = "ListInvoice_PDIRequestTransactions")]
    public class ListInvoice_PDIRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCODCLIENTE { get; set; }
        [DataMember]
        public string strIdSession { get; set; }
        [DataMember]
        public string strTransaction { get; set; }
        
       
    }
}
