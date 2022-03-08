using System;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion
{
    [DataContract]
    public class RegisterNuevaInteraccionPlusRequest : Claro.Entity.Request
    {
        [DataMember]
        public string txId { get; set; }
        [DataMember]
        public InteraccionPlus interaccionPlus { get; set; }
    }
}
