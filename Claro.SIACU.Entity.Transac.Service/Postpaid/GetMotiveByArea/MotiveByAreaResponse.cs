using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetMotiveByArea
{
    [DataContract(Name = "MotiveByAreaResponse")]
    public class MotiveByAreaResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstReasonByArea { get; set; }
    }
}
