using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultDistrict
{
    [DataContract(Name = "ConsultDistrictResponse")]
    public class ConsultDistrictResponse
    {
        [DataMember]
        public List<ConsultDistrict> ListConsultDistrict { get; set; }
    }
}
