using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.Legacy.GetTypeProductDat
{
    [DataContract]
    public class ResponseStatus
    {
        [DataMember(Name = "status")]
        public int status { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string codigoRespuesta { get; set; }
        [DataMember(Name = "descripcionRespuesta")]
        public string descripcionRespuesta { get; set; }
        [DataMember(Name = "ubicacionError")]
        public object ubicacionError { get; set; }
        [DataMember(Name = "fecha")]
        public DateTime fecha { get; set; }
        [DataMember(Name = "origen")]
        public object origen { get; set; }
    }
}
