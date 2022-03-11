using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "provisionarSuscripcionSNRequest")]
    public class ProvisionarSuscripcionSNRequest
    {
        [DataMember(Name = "operatorProvisioningProductRequest")]
        public OperatorProvisioningProductRequest operatorProvisioningProductRequest { get; set; }
    }
}
