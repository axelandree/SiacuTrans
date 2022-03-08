using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
    public class ConsultSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public ConsultSNHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultSNBodyResponse Body { get; set; }
    }
}
