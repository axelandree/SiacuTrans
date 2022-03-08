using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillData
{
    [DataContract(Name = "BillDataRequestPostPaid")]
    public class BillDataRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerCode { get; set; }
    } 
}
