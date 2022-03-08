using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetFixedCharge
{
    [DataContract(Name = "FixedChargeRequestTransactions")]
    public class FixedChargeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Flag { get; set; }
        [DataMember]
        public string Valor { get; set; }
    }
}
