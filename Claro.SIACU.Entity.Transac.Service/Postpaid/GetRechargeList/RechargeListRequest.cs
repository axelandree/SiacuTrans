using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetRechargeList
{
     [DataContract(Name = "RechargeListRequestTransactions")]
    public class RechargeListRequest : Claro.Entity.Request
    {
         [DataMember]
         public string vMSISDN { get; set; }
         [DataMember]
         public string vFECHINI { get; set; }
         [DataMember]
         public string vFECHFIN { get; set; }
         [DataMember]
         public string vFlag { get; set; }
         [DataMember]
         public int vNroRegistros { get; set; }
    }
}
