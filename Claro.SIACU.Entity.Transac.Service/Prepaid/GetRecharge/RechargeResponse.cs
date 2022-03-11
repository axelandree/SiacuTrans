using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetRecharge
{
    public class RechargeResponse
    {
        [DataMember]
        public Entity.Transac.Service.Prepaid.Recharge ObjRecharge { get; set; }

        [DataMember]
        public List<Entity.Transac.Service.Prepaid.Recharge> ListRecharge { get; set; }
    }
}
