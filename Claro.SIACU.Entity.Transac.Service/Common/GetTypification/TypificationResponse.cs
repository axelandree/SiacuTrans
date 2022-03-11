using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTypification
{
    [DataContract(Name = "TypificationResponse")]
    public class TypificationResponse
    {
        [DataMember]
        public Entity.Transac.Service.Common.Typification ObjTypification { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Common.Typification> ListTypification { get; set; }
    }
}
