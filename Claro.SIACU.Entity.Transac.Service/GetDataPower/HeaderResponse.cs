using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.GetDataPower
{
    [DataContract(Name = "HeaderResponse")]
    public class HeaderResponse
    { 
        [DataMember(Name = "consumer")]
        public string consumer { get; set; }
        [DataMember(Name = "pid")]
        public string pid { get; set; }
        [DataMember(Name = "timestamp")]
        public string timestamp { get; set; }
        [DataMember(Name = "varArg")]
        public string varArg { get; set; }
        [DataMember(Name = "status")]
        public HeaderStatusResponse status { get; set; }
    }
}
