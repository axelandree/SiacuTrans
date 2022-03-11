using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
    [DataContract]
    public class PersonalizaMensajeOTTResponse
    {
        [DataMember(Name = "MessageResponse")]
        public PersonalizaMensajeOTTMessageResponse MessageResponse { get; set; }
    }
}
