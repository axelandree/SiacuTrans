using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    [DataContract]
    public class UpdateClientSNResponse
    {
        [DataMember(Name = "MessageResponse")]
        public UpdateClientSNMessageResponse MessageResponse { get; set; }
    }
}
