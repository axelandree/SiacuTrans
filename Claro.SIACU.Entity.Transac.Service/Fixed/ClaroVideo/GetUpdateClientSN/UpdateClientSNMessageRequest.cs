using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    public class UpdateClientSNMessageRequest
    {
        [DataMember(Name = "Header")]
        public UpdateClientSNHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public UpdateClientSNBodyRequest Body { get; set; }
    }
}
