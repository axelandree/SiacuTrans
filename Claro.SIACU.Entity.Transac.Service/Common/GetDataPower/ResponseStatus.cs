using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDataPower
{
    public class ResponseStatus
    {
        [DataMember(Name = "estado")]
        public string estado { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }
        [DataMember(Name = "descripcionRespuesta")]
        public string descripcionRespuesta { get; set; }
        [DataMember(Name = "ubicacionError")]
        public string ubicacionError { get; set; }
        [DataMember(Name = "fecha")]
        public string fecha { get; set; }
        [DataMember(Name = "origen")]
        public string origen { get; set; }
    }
}
