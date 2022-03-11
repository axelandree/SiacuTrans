using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCurrentPlan
{
    [DataContract(Name = "CurrentPlanRequest")]
    public class CurrentPlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CoId { get; set; }
    }
}
