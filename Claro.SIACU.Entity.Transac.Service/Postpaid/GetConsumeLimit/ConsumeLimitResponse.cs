using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit
{
    [DataContract(Name = "ConsumeLimitResponseTransactions")]
    
    public class ConsumeLimitResponse
    {
        [DataMember]
        public List<Claro.SIACU.Entity.Transac.Service.Postpaid.ConsumeLimit> LstConsumeLimit { get; set; }
    }
}
