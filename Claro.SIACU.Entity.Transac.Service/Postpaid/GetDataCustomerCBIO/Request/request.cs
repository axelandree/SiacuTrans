using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request
{
    [DataContract]
    public class request //: Claro.Entity.Request
    {
        [DataMember]
        public obtenerDatosClienteRequestType obtenerDatosClienteRequest { get; set; }
    }
}
