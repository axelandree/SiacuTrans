using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
     [DataContract(Name = "PersonalizaMensajeOTTBodyResponse")]
    public class PersonalizaMensajeOTTBodyResponse
    {
        [DataMember(Name = "personalizarMensajeResponse")]
        public PersonalizarMensajeResponse personalizarMensajeResponse { get; set; }
    }
}
