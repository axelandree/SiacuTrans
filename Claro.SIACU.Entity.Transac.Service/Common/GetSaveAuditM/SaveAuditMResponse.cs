using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSaveAuditM
{
    [DataContract(Name = "SaveAuditMResponseCommon")]
    public class SaveAuditMResponse
    {
        [DataMember]
        public string vResultado { get; set; }
        [DataMember]
        public string vTransaccionResp { get; set; }
        [DataMember]
        public bool respuesta { get; set; }
    }
}
