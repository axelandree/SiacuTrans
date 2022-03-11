using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
    [DataContract(Name = "ConsultSNBodyResponse")]
    public class ConsultSNBodyResponse
    {

        [DataMember(Name = "queryOttResponse")]
        public QueryOttResponse queryOttResponse { get; set; }



        
    }
}
