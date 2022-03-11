using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterBonoSpeed
{
    [DataContract]
    public class GetRegisterBonoSpeedBodyResponse
    {
        [DataMember(Name = "mensajeError")]
        public string MensajeError { get; set; }
        [DataMember(Name = "codigoRespuesta")]
        public string CodigoRespuesta { get; set; }
        [DataMember(Name = "mensajeRespuesta")]
        public string MensajeRespuesta { get; set; }
    }
}
