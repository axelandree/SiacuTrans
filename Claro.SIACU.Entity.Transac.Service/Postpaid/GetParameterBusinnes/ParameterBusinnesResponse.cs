using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetParameterBusinnes
{
    [DataContract(Name = "ParameterBusinnesResponse")]
    public class ParameterBusinnesResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstParameterBusinnes { get; set; }
    }
}
