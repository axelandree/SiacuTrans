using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign
{
    [DataContract]
    public class ConsultCampaignResponse
    {
        [DataMember(Name = "auditResponse")]
        public GetConsultCampaignAuditResponse ConsultCampaignAuditResponse { get; set; }
        [DataMember(Name = "consultarCursor")]
        public GetConsultCampaignConsultCursor ConsultCampaignConsultCursor { get; set; }
    }
}
