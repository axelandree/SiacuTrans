using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAddressUpdate
{
    [DataContract(Name = "AddressUpdateFixedRequest")]
    public class AddressUpdateRequest:Claro.Entity.Request
    {
        [DataMember]
        public string strIdCustomer { get; set; }

        [DataMember]
        public string strDomicile { get; set; }

        [DataMember]
        public string strReference { get; set; }

        [DataMember]
        public string strDistrict { get; set; }

        [DataMember]
        public string strProvince { get; set; }

        [DataMember]
        public string StrDepartament { get; set; }
        [DataMember]
        public string strCodPostal { get; set; }

        [DataMember]
        public string strCountryLegal { get; set; }
     
    }
}
