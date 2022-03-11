using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
    [DataContract]
    public class PersonalizaMensajeOTTRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public PersonalizaMensajeOTTMessageRequest MessageRequest { get; set; } 
    }
}
