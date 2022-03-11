using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetPersonalizaMensajeOTT
{
     [DataContract(Name = "PersonalizaMensajeOTTBodyRequest")]
    public class PersonalizaMensajeOTTBodyRequest
    {
         [DataMember(Name = "personalizarMensajeRequest")]
         public PersonalizarMensajeRequest personalizarMensajeRequest { get; set; }
    }
}
