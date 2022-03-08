using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "MessageResponse")]
        public MessageResponse MessageResponse { get; set; }
    }
}
