using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetEquipmentByCurrentPlan
{
    [DataContract(Name = "EquipmentsByCurrentPlanRequest")]
    public class EquipmentsByCurrentPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strIdContract { get; set; }
    }
}
