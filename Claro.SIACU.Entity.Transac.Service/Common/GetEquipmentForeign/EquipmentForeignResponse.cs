using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetEquipmentForeign
{
    [DataContract]
    public class EquipmentForeignResponse
    {
        [DataMember]
        public List<EquipmentForeign> ListEquipmentForeign { get; set; }

        [DataMember]
        public int CodeError { get; set; }

        [DataMember]
        public string MsgError { get; set; }
    }
}
