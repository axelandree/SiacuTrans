using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidationProgDeudaBloqSusp
{
    [DataContract(Name = "ValidationProgDeudaBloqSuspResponse")]
    public class ValidationProgDeudaBloqSuspResponse
    {
        [DataMember]
        public string RespuestaValidacion { get; set; }
    }
}
