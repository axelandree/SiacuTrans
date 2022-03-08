using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp
{
    [DataContract(Name = "ValidationProgDeudaBloqSuspRequest")]
    public class ValidationProgDeudaBloqSuspRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Telefono { get; set; }

        [DataMember]
        public string Contrato { get; set; }

        [DataMember]
        public string PRY { get; set; }
    }
}
