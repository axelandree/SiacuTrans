using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
    [DataContract]
    public class ConsultSNResponse
    {
        [DataMember(Name = "MessageResponse")]
        public ConsultSNMessageResponse MessageResponse { get; set; }

    }
}
