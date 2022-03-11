using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillData
{  
    [DataContract(Name = "BillDataResponsePostPaid")]
    public class BillDataResponse : Claro.Entity.Request
    {
        [DataMember]
        public List<ListItem> ListBillSummary { get; set; }

        [DataMember]
        public string MsgError { get; set; }
    }
}
