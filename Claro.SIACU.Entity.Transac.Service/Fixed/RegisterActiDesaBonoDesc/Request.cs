using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class Request : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public MessageRequest MessageRequest { get; set; }
    }
}
