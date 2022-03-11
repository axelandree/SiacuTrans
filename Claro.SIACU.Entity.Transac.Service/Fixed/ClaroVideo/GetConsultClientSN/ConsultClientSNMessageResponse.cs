using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    public class ConsultClientSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public ConsultClientSNHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ConsultClientSNBodyResponse Body { get; set; }
    }
}
