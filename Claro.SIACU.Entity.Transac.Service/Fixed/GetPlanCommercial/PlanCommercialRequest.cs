using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetPlanCommercial
{
    [DataContract]
    public class PlanCommercialRequest : Claro.Entity.Request
    {
        [DataMember]
        public int StrContractId { get; set; }
        [DataMember]
        public int StrCodService { get; set; }
        [DataMember]
        public int StrState { get; set; }
        [DataMember]
        public string StrTypeProduct { get; set; }
    }
}
