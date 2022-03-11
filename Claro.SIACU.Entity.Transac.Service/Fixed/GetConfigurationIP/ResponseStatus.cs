using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetConfigurationIP
{
    [DataContract]
    public class ResponseStatus
    {
        [DataMember]
        public int intEstado { get; set; }
        [DataMember]
        public string strCodigoRespuesta { get; set; }
        [DataMember]
        public string strDescripcionRespuesta { get; set; }
        [DataMember]
        public string strUbicacionError { get; set; }
        [DataMember]
        public Nullable<System.DateTime> dFecha { get; set; }
        [DataMember]
        public string strOrigen { get; set; }
    }
}
