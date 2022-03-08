using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetTarifePlan
{
    [DataContract(Name = "TarifePlanRequest")]
    public class TarifePlanRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strTPROCCode { get; set; }
        [DataMember]
        public string strPRDCCode { get; set; }
        [DataMember]
        public string strModalVent { get; set; }
        [DataMember]
        public string strPLNCFamilly { get; set; }
    }
}
