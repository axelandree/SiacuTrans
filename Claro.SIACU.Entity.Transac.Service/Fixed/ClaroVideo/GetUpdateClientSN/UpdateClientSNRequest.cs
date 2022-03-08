using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    [DataContract]
    public class UpdateClientSNRequest : Tools.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public UpdateClientSNMessageRequest MessageRequest { get; set; }
    }
}
