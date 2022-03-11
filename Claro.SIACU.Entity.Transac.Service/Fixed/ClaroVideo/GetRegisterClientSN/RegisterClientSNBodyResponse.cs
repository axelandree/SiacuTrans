using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    [DataContract(Name = "RegisterClientSNBodyResponse")]
    public class RegisterClientSNBodyResponse
    {
        [DataMember(Name = "createUserOttResponse")]
        public CreateUserOttResponse createUserOttResponse { get; set; }
    }
}
