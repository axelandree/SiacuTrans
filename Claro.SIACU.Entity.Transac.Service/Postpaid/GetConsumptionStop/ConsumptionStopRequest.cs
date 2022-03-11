using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop
{
    [DataContract(Name = "ConsumptionStopRequest")]
    public class ConsumptionStopRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCode { get; set; }
    }
}
