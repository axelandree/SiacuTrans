using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultDistrict
{
    [DataContract(Name = "ConsultDistrictRequest")]
    public class ConsultDistrictRequest : Claro.Entity.Request
    {
        [DataMember]
        public int CodProvince { get; set; }
        [DataMember]
        public int CodDistrict { get; set; }
        [DataMember]
        public int CodState { get; set; }
    }
}
