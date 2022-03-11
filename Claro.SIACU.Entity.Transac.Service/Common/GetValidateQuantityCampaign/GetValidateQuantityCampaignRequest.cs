using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class GetValidateQuantityCampaignRequest
    {
        [DataMember(Name = "MessageRequest")]
        public GetValidateQuantityCampaignMessageRequest ValidateQuantityCampaignMessageRequest { get; set; } 
    }
}
