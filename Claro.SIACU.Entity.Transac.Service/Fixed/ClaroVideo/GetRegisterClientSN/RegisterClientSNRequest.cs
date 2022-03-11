using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    [DataContract]
    public class RegisterClientSNRequest: Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public RegisterClientSNMessageRequest MessageRequest { get; set; }
    }
}
