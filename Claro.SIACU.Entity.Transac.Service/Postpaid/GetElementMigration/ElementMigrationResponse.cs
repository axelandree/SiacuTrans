using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration
{
    [DataContract(Name = "ElementMigrationResponse")]
    public class ElementMigrationResponse
    {
        [DataMember]
        public List<Entity.Transac.Service.Postpaid.ParameterBusinnes> lstParameterBusinnes { get; set; }
    }
}
