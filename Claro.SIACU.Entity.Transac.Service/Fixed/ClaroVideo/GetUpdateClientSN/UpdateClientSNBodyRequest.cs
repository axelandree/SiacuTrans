using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    [DataContract(Name = "UpdateClientSNBodyRequest")]
    public class UpdateClientSNBodyRequest
    {
        [DataMember(Name = "updateUserOttRequest")]
        public UpdateUserOttRequest updateUserOttRequest { get; set; }
    }
}
