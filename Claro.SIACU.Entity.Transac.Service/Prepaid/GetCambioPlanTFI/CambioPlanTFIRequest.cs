using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Prepaid.GetCambioPlanTFI
{
    [DataContract]
    public class CambioPlanTFIRequest: Claro.Entity.Request
    {
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string offer { get; set; }
        [DataMember]
        public string subscriberStatus { get; set; }
    }
}
