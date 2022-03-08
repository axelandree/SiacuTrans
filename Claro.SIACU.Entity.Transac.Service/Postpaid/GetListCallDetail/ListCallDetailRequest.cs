using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetail
{
    [DataContract(Name = "ListCallDetailRequestTransactions")]
    public class ListCallDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vINVOICENUMBER { get; set; }
        [DataMember]
        public string vTELEFONO { get; set; }
        [DataMember]
        public string vSeguridad { get; set; }
    }
}
