using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetUpdateClientSN
{
    public class UpdateClientSNHeaderRequest
    {
        [DataMember(Name = "HeaderRequest")]
        public GetDataPower.HeaderRequest HeaderRequest { get; set; }
    }
}
