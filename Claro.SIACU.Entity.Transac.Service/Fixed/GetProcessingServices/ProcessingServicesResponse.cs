using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetProcessingServices
{
    [DataContract]
    public class ProcessingServicesResponse
    {
        [DataMember]
        public bool rResult { get; set; }
        [DataMember]
        public string rResultado { get; set; }
        [DataMember]
        public string rMensaje { get; set; }
    }
}
