using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
    public class PersonalizaMensajeOTTMessageRequest
    {
        [DataMember(Name = "Header")]
        public PersonalizaMensajeOTTHeaderRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public PersonalizaMensajeOTTBodyRequest Body { get; set; }
    }
}
