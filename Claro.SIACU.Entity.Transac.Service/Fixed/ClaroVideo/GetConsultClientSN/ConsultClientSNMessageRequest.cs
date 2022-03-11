using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    public class ConsultClientSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultClientSNHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultClientSNBodyRequest Body { get; set; }
    }
}
