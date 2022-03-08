using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProcessingServices
{
    [DataContract]
    public class ProcessingServicesRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vCoId { get; set; }
        [DataMember]
        public string vCustomerId { get; set; }
        [DataMember]
        public string vCadena { get; set; }
    }
}
