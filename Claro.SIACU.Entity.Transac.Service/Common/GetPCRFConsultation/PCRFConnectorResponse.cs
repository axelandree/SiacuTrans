using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetPCRFConsultation
{
    [DataContract]
    public class PCRFConnectorResponse
    {
        [DataMember]
        public List<SuscriberQuota> listSuscriberQuota { get; set; }
        [DataMember]
        public string codRespuesta { get; set; }
        [DataMember]
        public string msjRespuesta { get; set; }

        [DataMember]
        public string strTelefonoLTE { get; set; }
    }
}
