using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumeLimit
{
    [DataContract(Name = "ConsumeLimitRequestTransactions")]
    public class ConsumeLimitRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telefono { get; set; }
        [DataMember]
        public string IdContrato { get; set; }
    }
}
