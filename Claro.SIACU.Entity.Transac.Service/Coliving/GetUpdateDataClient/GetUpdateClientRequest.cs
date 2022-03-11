using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetUpdateClientRequest
    {
        [DataMember(Name = "clienteID")]
        public string clienteID { get; set; }
        [DataMember(Name = "tipoDocumento")]
        public string tipoDocumento { get; set; }
        [DataMember(Name = "numeroDocumento")]
        public string numeroDocumento { get; set; }
        [DataMember(Name = "codAplicacion")]
        public string codAplicacion { get; set; }
        [DataMember(Name = "sistemaOrigen")]
        public string sistemaOrigen { get; set; }
        [DataMember(Name = "usuario")]
        public string usuario { get; set; }
        [DataMember(Name = "motivo")]
        public string motivo { get; set; }
        [DataMember(Name = "dirSecuencial")]
        public string dirSecuencial { get; set; }
        [DataMember(Name = "csId")]
        public string csId { get; set; }
        [DataMember(Name = "csIdPub")]
        public string csIdPub { get; set; }

        [DataMember(Name = "datosCliente")]
        public GetDataClientRequest GetDataClientRequest { get; set; }
        [DataMember(Name = "listaDirecciones")]
        public List<GetListAddressRequest> GetListAddressRequest { get; set; }
        [DataMember(Name = "listaCuentaFacturacion")]
        public List<GetAccountBillingRequest> GetAccountBillingRequest { get; set; }
        [DataMember(Name = "listaDatosCuentaFacturacion")]
        public List<GetListDataAccountBillingRequest> GetListAccountBillingRequest { get; set; }
        [DataMember(Name = "listaOpcional")]
        public List<ListOptional> ListOptional { get; set; }
        [DataMember(Name = "listaRepresentanteLegal")]
        public List<GetListRepresentanteLegal> GetListRepresentanteLegal { get; set; }



    }
}
