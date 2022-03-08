using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEvaluateAmount
{
    [DataContract]
    public class EvaluateAmountResponse
    {
        [DataMember]
        public bool Resultado { get; set; }
    }
}
