using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    public class RegisterClientSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public RegisterClientSNHeaderResponse Header { get; set; }

        [DataMember(Name = "Body")]
        public RegisterClientSNBodyResponse Body { get; set; }
    }
}
