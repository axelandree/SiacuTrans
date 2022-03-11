using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class ServiceBSCS
    {
        [DataMember]
        public string StrService { get; set; }
        [DataMember]
        public string StrPackage { get; set; }
        [DataMember]
        public string StrStatus { get; set; }
    }
}
