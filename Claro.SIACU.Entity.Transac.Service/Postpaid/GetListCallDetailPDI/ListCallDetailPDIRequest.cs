using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetListCallDetailPDI
{
    [DataContract(Name = "ListCallDetailPDIRequestTransactions")]
    public class ListCallDetailPDIRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vINVOICENUMBER { get; set; }
        [DataMember]
        public string vTELEFONO { get; set; }
        [DataMember]
        public string vSeguridad { get; set; }
    }
}
