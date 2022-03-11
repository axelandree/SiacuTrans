using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetUpdateInter29
{
    [DataContract]
   public class UpdateInter29Request: Claro.Entity.Request
    
    {
        [DataMember]
        public string p_objid { get; set; }
        [DataMember]
        public string p_texto { get; set; }
        [DataMember]
        public string p_orden { get; set; }
    }
}
