using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.GetDataPower
{
    [DataContract(Name = "Fault")]
    public class FaultResponse
    {
        [DataMember(Name = "faultcode")]
        public string faultcode { get; set; }

        [DataMember(Name = "faultstring")]
        public string faultstring { get; set; }

        [DataMember(Name = "faultactor")]
        public string faultactor { get; set; }

        [DataMember(Name = "detail")]
        public FaultDetail detail { get; set; }

    }

    [DataContract(Name = "FaultDetail")]
    public class FaultDetail
    {
        [DataMember(Name = "IntegrationError")]
        public string IntegrationError { get; set; }

    }
}
