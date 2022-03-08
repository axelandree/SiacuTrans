using Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Common;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetDataCustomerCBIO.Request
{
    [DataContract]
    public class obtenerDatosClienteRequestType
    {
        [DataMember]
        public string custCode { get; set; }
        [DataMember]
        public string dnNum { get; set; }
        [DataMember]
        public List<listaOpcional> listaOpcional { get; set; }
    }

}


