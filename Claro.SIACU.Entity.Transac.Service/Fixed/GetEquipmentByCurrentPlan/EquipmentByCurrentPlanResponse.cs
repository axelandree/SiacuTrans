using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan
{
    [DataContract(Name = "EquipmentsByCurrentPlanResponse")]
    public class EquipmentsByCurrentPlanResponse
    {
        [DataMember]
        public List<EquipmentByCurrentPlan> Equipments { get; set; }
    }
}
