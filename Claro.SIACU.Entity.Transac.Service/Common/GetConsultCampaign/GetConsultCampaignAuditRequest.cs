using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign
{
    [DataContract]
    public class GetConsultCampaignAuditRequest
    {
        [DataMember(Name = "idTransaccion")]
        public string idTransaction { get; set; }
        [DataMember(Name = "ipAplicacion")]
        public string IpAplicacion { get; set; }
        [DataMember(Name = "nombreAplicacion")]
        public string NameApplication { get; set; }
        [DataMember(Name = "usuarioAplicacion")]
        public string UserAplication { get; set; }
    }
}
