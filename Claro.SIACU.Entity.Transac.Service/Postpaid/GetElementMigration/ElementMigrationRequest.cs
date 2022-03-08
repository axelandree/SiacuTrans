using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetElementMigration
{
    [DataContract(Name = "ElementMigrationRequest")]
    public class ElementMigrationRequest : Claro.Entity.Request
    {
        [DataMember]
        public int intCode {get; set; }
    }
}
