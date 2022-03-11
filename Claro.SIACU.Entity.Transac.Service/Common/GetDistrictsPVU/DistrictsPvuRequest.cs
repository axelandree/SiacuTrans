using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetDistrictsPVU
{
     
    [DataContract(Name = "DistrictsPvuRequestCommon")]
    public class DistrictsPvuRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodProv { get; set; }
        [DataMember]
        public string CodDistr { get; set; }
        [DataMember]
        public string CodDepart { get; set; }
        [DataMember]
        public string CodStatus { get; set; }
    }
}
