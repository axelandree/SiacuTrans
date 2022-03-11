using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
    public class PersonalizaMensajeOTTMessageResponse
    {
        [DataMember(Name = "Header")]
        public PersonalizaMensajeOTTHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public PersonalizaMensajeOTTBodyResponse Body { get; set; }
    }
}
