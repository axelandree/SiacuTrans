using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetBillingResponse
    {
        [DataMember(Name = "listaCuentaFacturacion")]
        public List<GetAccountBillingResponse> LstAccountBillingResponse { get; set; }
        [DataMember(Name = "listaDatosCuentaFacturacion")]
        public List<GetDataAccountBillingResponse> LstDataAccountBillingResponse { get; set; }
        [DataMember(Name = "listaDireccionesFacturacion")]
        public List<GetAddressBillingResponse> LstAddressBillingResponse { get; set; }
        [DataMember(Name = "responseAudit")]
        public AuditResponse AuditResponse { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<ListOptional> ListOptional { get; set; }
    }
}
