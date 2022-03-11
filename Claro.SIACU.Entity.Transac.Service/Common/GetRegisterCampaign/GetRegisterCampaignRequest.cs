using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign
{
    [DataContract]
    public class GetRegisterCampaignRequest
    {
        [DataMember(Name = "MessageRequest")]
        public GetRegisterCampaignMessageRequest RegisterCampaignMessageRequest { get; set; }
    }
}
