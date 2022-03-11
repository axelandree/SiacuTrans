using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetDataBillingResponse
    {
        [DataMember(Name = "obtenerDatosFacturacionResponseType")]
        public GetBillingResponse GetBillingResponse { get; set; }

    }
}
