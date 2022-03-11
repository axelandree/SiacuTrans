using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetOffice
{
    [DataContract(Name = "OfficeRequestCommon")]
    public class OfficeRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCodeUser { get; set; }
    }
}
