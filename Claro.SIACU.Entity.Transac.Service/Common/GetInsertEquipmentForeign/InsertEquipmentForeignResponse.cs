using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetInsertEquipmentForeign
{
    [DataContract]
    public class InsertEquipmentForeignResponse
    {
        [DataMember]
        public bool ProcesSucess { get; set; }

        [DataMember]
        public int codeResult { get; set; }

        [DataMember]
        public string msgResult { get; set; }

        [DataMember]
        public int codeFailed { get; set; }

        [DataMember]
        public string namePDF { get; set; }
        

  
    }
}
