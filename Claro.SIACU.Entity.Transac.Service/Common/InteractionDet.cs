using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service
{
    [DataContract]
  public class InteractionDet
    {
        
        [DataMember]
        public string object_id{get; set;}
        [DataMember]
        public string product_id { get; set; }
        [DataMember]
        public string product_type { get; set; }
        [DataMember]
        public string product_name { get; set; }
        [DataMember]
        public string campaign { get; set; }
        [DataMember]
        public string points { get; set; }
        [DataMember]
        public string amount { get; set; }
        [DataMember]
        public string flg_residue { get; set; }
        [DataMember]
        public string recharge_repayment { get; set; }
        [DataMember]
        public string application_id { get; set; }
    }
}
