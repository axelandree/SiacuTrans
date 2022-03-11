using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBusinessRules
{
    public class BusinessRulesResponse
    {
        [DataMember]
        public Entity.Transac.Service.Common.BusinessRules ObjBusinessRules { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Common.BusinessRules> ListBusinessRules { get; set; }
    }
}
