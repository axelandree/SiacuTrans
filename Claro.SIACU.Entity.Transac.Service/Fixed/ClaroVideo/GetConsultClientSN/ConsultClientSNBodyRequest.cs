using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    [DataContract(Name = "ConsultClientSNBodyRequest")]
    public class ConsultClientSNBodyRequest
    {
        [DataMember(Name = "queryUserOttRequest")]
        public QueryUserOttRequest queryUserOttRequest { get; set; }
    }
}
