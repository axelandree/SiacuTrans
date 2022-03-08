using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    [DataContract]
    public class ConsultClientSNResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ConsultClientSNMessageResponse MessageResponse { get; set; }
    }
}
