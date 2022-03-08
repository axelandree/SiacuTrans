using System;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion
{
    [DataContract]
    public class RegisterNuevaInteraccionRequest : Claro.Entity.Request
    {
        [DataMember]
        public string txId { get; set; }
        [DataMember]
        public Interaccion interaccion { get; set; }
    }
}
