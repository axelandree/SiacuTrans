using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.BannerDesc
{
    [DataContract]
    public class ResponseAudi
    {
        [DataMember]
        public string idTransaccion { get; set; }
        [DataMember]
        public string codigoRespuesta { get; set; }
        [DataMember]
        public string mensajeRespuesta { get; set; }
    }
}
