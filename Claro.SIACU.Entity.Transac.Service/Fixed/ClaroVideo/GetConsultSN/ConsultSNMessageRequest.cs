using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
    public class ConsultSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public ConsultSNHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultSNBodyRequest Body { get; set; }
    }
}
