using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetHLR
{
    public class HLRResponse
    {
        [DataMember]
        public Entity.Transac.Service.Postpaid.HLR ObjHLR { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Postpaid.HLR> ListHLR { get; set; }

        [DataMember]
        public int LOCATION { get; set; }
    }
}
