using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetBlackList
{
    [DataContract]
    public class BlackListBodyRequest
    {
        [DataMember(Name = "pi_imei")]
        public string pi_imei { get; set; }
    }
}
