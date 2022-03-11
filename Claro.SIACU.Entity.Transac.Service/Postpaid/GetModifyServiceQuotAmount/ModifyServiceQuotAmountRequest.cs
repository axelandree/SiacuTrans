using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyServiceQuotAmount
{
    [DataContract(Name = "ModifyServiceQuotAmountRequest")]
    public class ModifyServiceQuotAmountRequest : Claro.Entity.Request
    {
        [DataMember]
        public int IntCodId { get; set; }
        [DataMember]
        public int IntSnCode { get; set; }
        [DataMember]
        public int IntSpCode { get; set; }
        [DataMember]
        public double DCost { get; set; }
        [DataMember]
        public int IntPeriod { get; set; }

    }
}
