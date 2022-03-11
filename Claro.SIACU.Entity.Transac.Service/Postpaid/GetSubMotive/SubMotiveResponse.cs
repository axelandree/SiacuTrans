using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetSubMotive
{
    [DataContract(Name = "SubMotiveResponse")]
    public class SubMotiveResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstSubMotive { get; set; }
    }
}
