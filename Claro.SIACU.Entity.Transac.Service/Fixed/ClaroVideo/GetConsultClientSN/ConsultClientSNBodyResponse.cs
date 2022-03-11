using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultClientSN
{
    [DataContract(Name = "ConsultClientSNBodyResponse")]
    public class ConsultClientSNBodyResponse
    {
        [DataMember(Name = "queryUserOttResponse")]
        public QueryUserOttResponse queryUserOttResponse { get; set; }

    }
}
