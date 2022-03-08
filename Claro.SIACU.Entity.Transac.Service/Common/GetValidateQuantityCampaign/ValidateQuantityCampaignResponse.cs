using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class ValidateQuantityCampaignResponse
    {
        [DataMember(Name = "auditResponse")]
        public GetValidateQuantityCampaignAuditResponse ValidateQuantityCampaignAuditResponse { get; set; }
        [DataMember(Name = "listarCantMaxProducto")]
        public GetValidateQuantityCampaignListCantMaxProductResponse ValidateQuantityCampaignListCantMaxProductResponse { get; set; } 
    }
}
