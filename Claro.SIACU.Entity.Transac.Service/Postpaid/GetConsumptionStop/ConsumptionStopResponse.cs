using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetConsumptionStop
{
    [DataContract(Name = "ConsumptionStopResponse")]
    public class ConsumptionStopResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstConsumptionStop { get; set; }
    }
}
