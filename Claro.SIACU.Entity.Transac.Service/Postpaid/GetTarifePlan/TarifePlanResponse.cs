using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan
{
    [DataContract(Name = "TarifePlanResponse")]
    public class TarifePlanResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.TarifePlan> lstTarifePlan { get; set; }
    }
}
