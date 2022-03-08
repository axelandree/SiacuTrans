using System;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterNuevaInteraccion
{
    [DataContract]
    public class RegisterNuevaInteraccionResponse
    {
        [DataMember]
        public Audit audit { get; set; }
        [DataMember]
        public string interaccionId { get; set; }
    }
}
