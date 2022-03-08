using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class GetValidateQuantityCampaignListCantMaxProductResponse
    {
        [DataMember(Name = "tipoProducto")]
        public List<GetValidateQuantityCampaignKindProductResponse> ValidateQuantityCampaignKindProductResponse { get; set; } 
    }
}
