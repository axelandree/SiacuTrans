using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEvaluateAmount
{
    [DataContract]
    public class EvaluateAmountRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrIdSession { get; set; }
        [DataMember]
        public string StrTransaction { get; set; }
        [DataMember]
        public string VListaPerfil { get; set; }
        [DataMember]
        public double VMonto { get; set; }
        [DataMember]
        public string VUnidad { get; set; }
        [DataMember]
        public string VModalidad { get; set; }
        [DataMember]
        public string VTipoTelefono { get; set; }
    }
}
