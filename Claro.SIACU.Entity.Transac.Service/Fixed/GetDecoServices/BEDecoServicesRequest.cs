using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetDecoServices
{
    [DataContract(Name = "BEDecoServicesRequestFixed")]
    public class BEDecoServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCusID { get; set; }
        [DataMember]
        public string vCoID { get; set; }

    }
}
