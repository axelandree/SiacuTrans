using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPuntosAtencion
{
    [DataContract(Name = "PuntosAtencionResponse")]
    public class PuntosAtencionResponse : Claro.Entity.Response
    {
        [DataMember(Name="Headers")]
        public GetDataPower.HeadersResponse HeadersResponse { get; set; }
        //[DataMember(Name = "header")]
        //public headerResponse headerResponse { get; set; }
        [DataMember]
        public obtenerTabPuntosAtencionPostResponse obtenerTabPuntosAtencionPostResponse { get; set; }
    }
}
