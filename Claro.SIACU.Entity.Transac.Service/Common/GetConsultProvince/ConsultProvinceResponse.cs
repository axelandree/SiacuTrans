using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultProvince
{
    [DataContract(Name = "ConsultProvinceResponse")]
    public class ConsultProvinceResponse
    {
        [DataMember]
        public List<ConsultProvince> ListConsultProvince { get; set; }
    }
}
