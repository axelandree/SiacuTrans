using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterInteractionAdjust
{
    [DataContract]
    public class ActualizarCamposSap
    {
        [DataMember]
        public string piFlagEnvioSap {get; set;}
        [DataMember]
        public string piErrorWsSap { get; set; }
        [DataMember]
        public string piReintentosWsSap {get; set;}
    }
}
