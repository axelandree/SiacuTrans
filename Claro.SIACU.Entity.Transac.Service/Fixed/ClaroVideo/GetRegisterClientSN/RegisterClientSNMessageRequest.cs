using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    public class RegisterClientSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public RegisterClientSNHeaderRequest Header { get; set; }

        [DataMember(Name = "Body")]
        public RegisterClientSNBodyRequest Body { get; set; }
    }
}
