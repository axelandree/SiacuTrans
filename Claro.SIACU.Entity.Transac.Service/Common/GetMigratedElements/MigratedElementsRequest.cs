using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetMigratedElements
{
    [DataContract(Name = "MigratedElementsRequest")]
    public class MigratedElementsRequest : Claro.Entity.Request
    {
        [DataMember]
        public Int64 Id { get; set; }
        [DataMember]
        public string NameFunction { get; set; }
    }
}
