using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetValidateQuantityCampaign
{
    [DataContract]
    public class GetValidateQuantityCampaignBodyResponse
    {
        [DataMember(Name = "validarCantidadCampaniaResponse")]
        public ValidateQuantityCampaignResponse ValidateQuantityCampaignResponse { get; set; } 
    }
}
