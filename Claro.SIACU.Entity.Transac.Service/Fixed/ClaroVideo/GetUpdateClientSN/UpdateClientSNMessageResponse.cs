using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    public class UpdateClientSNMessageResponse
    {
        [DataMember(Name = "Header")]
        public UpdateClientSNHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public UpdateClientSNBodyResponse Body { get; set; }
    }
}
