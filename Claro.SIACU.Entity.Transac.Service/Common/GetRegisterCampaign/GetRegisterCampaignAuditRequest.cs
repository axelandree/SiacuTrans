using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign
{
    [DataContract]
    public class GetRegisterCampaignAuditRequest
    {
        [DataMember(Name = "idTransaccion")]
        public string IdTransaction { get; set; }
        [DataMember(Name = "ipAplicacion")]
        public string IpAplication { get; set; }
        [DataMember(Name = "nombreAplicacion")]
        public string NameAplication { get; set; }
        [DataMember(Name = "usuarioAplicacion")]
        public string UserAplication { get; set; }
    }
}
