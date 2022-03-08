using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetProvincesPVU
{

    [DataContract(Name = "ProvincesPvuRequestCommon")]
    public class ProvincesPvuRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CodProv { get; set; }
        [DataMember]
        public string CodDep { get; set; }
        [DataMember]
        public string CodStatus { get; set; }
    }
}
