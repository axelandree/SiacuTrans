using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetListEquipmentRegistered
{
    public class ListEquipmentRegisteredResponse
    {
          
        [DataMember]
        public List<ListEquipmentRegistered> ListEquipmentRegistered { get; set; }

        [DataMember]
        public int CodeError { get; set; }

        [DataMember]
        public string MsgError { get; set; }
    }

    
}
