using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
      [DataContract(Name = "UpdateClientSNBodyResponse")]
    public class UpdateClientSNBodyResponse
    {
          [DataMember(Name = "updateUserOttResponse")]
          public UpdateUserOttResponse updateUserOttResponse { get;set; }
    }
}
