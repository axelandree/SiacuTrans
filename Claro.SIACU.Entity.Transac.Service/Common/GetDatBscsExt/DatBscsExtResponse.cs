using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDatBscsExt
{
    [DataContract]
    public class DatBscsExtResponse
    {
        [DataMember]
        public string NroTelephone { get; set; }
        [DataMember]
        public string CodeNewPlan { get; set; }
        [DataMember]
        public double NroFacture { get; set; }
        [DataMember]
        public double CargCurrentFixed { get; set; }
        [DataMember]
        public double CargFixedNewPlan { get; set; }
        [DataMember]
        public bool Result { get; set; }
    }   
}       
