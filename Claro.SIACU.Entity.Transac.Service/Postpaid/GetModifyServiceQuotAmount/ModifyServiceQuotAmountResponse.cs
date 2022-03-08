using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetModifyServiceQuotAmount
{
    [DataContract(Name = "ModifyServiceQuotAmountResponse")]
    public class ModifyServiceQuotAmountResponse
    {
        [DataMember]
        public string Result { get; set; }
    }
}
