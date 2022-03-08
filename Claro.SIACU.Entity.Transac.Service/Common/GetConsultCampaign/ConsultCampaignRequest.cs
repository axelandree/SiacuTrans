using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetConsultCampaign
{
    [DataContract]
    public class ConsultCampaignRequest
    {
        [DataMember(Name = "auditRequest")]
        public GetConsultCampaignAuditRequest ConsultCampaignAuditRequest { get; set; }
        [DataMember(Name = "consultaCampania")]
        public GetConsultCampaign ConsultCampaign { get; set; }
    }
}
