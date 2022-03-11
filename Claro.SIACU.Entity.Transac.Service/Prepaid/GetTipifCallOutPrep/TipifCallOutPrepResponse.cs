using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetTipifCallOutPrep
{
    [DataContract(Name = "TipifCallOutPrepResponse")]
   public  class TipifCallOutPrepResponse
    {

        [DataMember]
        public string InteraccionId { get; set; }
        [DataMember]
        public string TipoId{ get; set; }
        [DataMember]
        public string TipoDes { get; set; }
        [DataMember]
        public string ClaseId { get; set; }
        [DataMember]
        public string SubClaseId { get; set; }

        [DataMember]
        public string ClaseDes { get; set; }
        [DataMember]
        public string SubClaseDes { get; set; }
        [DataMember]
        public string RespTipif { get; set; }
        [DataMember]
        public string DescTipif { get; set; }
    }
}
