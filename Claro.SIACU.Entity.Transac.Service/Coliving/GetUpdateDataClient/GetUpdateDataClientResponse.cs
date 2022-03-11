using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetUpdateDataClient
{
    [DataContract]
    public class GetUpdateDataClientResponse
    {
        [DataMember(Name = "actualizarDatosClienteResponse")]
        public GetUpdateClientResponse GetUpdateClientResponse { get; set; }
    }
}
