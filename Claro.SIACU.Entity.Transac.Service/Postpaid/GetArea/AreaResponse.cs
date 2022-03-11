using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetArea
{
    [DataContract(Name = "AreaResponse")]
    public class AreaResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstArea { get; set; }
    }
}
