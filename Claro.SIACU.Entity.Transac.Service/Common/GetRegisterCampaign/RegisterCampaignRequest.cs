using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign
{
    [DataContract]
    public class RegisterCampaignRequest
    {
        [DataMember(Name = "auditRequest")]
        public GetRegisterCampaignAuditRequest RegisterCampaignAuditRequest { get; set; }
        [DataMember(Name = "registrarCampania")]
        public GetRegisterCampaing RegisterCampaign { get; set; }
    }
}
