using Claro.SIACU.Entity.Transac.Service.Common.GetInsertInteractHFC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetConfigurationIP
{
    [DataContract]
    public class ConfigurationIPResponse
    {
        [DataMember]
        public ResponseStatus oResponseStatus { get; set; }
        [DataMember]
        public string strCodigoRespuesta { get; set; }
        [DataMember]
        public string strDescripcionRespuesta { get; set; }
        [DataMember]
        public string strNroSOT { get; set; }
        [DataMember]
        public string  strRutaConstacy { get; set; }
        [DataMember]
        public string strIdInteraccion { get; set; }
        [DataMember]
        public string strflag { get; set; }
        public ConfigurationIPResponse() {
            oResponseStatus = new ResponseStatus();
        }
    }
}
