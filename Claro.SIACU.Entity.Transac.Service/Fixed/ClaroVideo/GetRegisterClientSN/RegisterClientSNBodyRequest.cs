using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetRegisterClientSN
{
    [DataContract(Name = "RegisterClientSNBodyRequest")]
    public class RegisterClientSNBodyRequest
    {
        [DataMember(Name = "createUserOttRequest")]
        public CreateUserOttRequest createUserOttRequest { get; set; }

    }
}
