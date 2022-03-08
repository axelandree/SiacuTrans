using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPuntosAtencion
{
    [DataContract(Name = "PuntosAtencionRequest")]
    public class PuntosAtencionRequest : Claro.Entity.Request
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeaderRequest Header { get; set; }
        [DataMember (Name ="TabPuntosAtencion")]
        public obtenerTabPuntosAtencionPostRequest obtenerTabPuntosAtencionPostRequest {get; set; }

    }
}
