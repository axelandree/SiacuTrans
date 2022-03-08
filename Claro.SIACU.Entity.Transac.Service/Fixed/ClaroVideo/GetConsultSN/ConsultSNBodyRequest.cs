using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo.GetConsultSN
{
    [DataContract(Name = "ConsultSNBodyRequest")]
    public class ConsultSNBodyRequest    {

        [DataMember(Name = "queryOttRequest")]
        public QueryOttRequest QueryOttRequest { get; set; }
    }
}
