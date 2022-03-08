using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetIdUbigeo
{
    [DataContract(Name="IdUbigeoFixedRequest")]
    public class IdUbigeoRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strDepartment { get; set; }
        [DataMember]
        public string strState { get; set; }
        [DataMember]
        public string strProvince { get; set; }
        [DataMember]
        public string strDistrict { get; set; }
    }
}
