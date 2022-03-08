using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetDataBillingRequest
    {
        [DataMember(Name = "obtenerDatosFacturacionRequestType")]
        public GetBillingRequest GetBillingRequest { get; set; }

    }
}
