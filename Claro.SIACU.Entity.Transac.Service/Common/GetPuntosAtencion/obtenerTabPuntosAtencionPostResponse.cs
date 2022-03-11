using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPuntosAtencion
{
    [DataContract]
    public class obtenerTabPuntosAtencionPostResponse 
    {
        [DataMember(Name = "ResponseStatus")]
        public GetDataPower.ResponseStatus ResponseStatus { get; set; }
        [DataMember(Name = "responseDataObtenerTabPuntosAtencion")]
        public responseDataObtenerTabPuntosAtencionPost responseDataObtenerTabPuntosAtencionPost { get; set; }

    }
}
