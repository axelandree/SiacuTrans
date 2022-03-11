using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetQueryAssociatedLines
{
    [DataContract(Name = "QueryAssociatedLinesRequestPostPaid")]
    public class QueryAssociatedLinesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Date_Ini { get; set; }
        [DataMember]
        public string Date_End { get; set; }
        [DataMember]
        public int TypeQuery { get; set; }
    }
}
