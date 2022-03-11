using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign
{
    [DataContract]
    public class GetRegisterCampaignBodyResponse
    {
        [DataMember(Name = "registrarCampaniaResponse")]
        public RegisterCampaignResponse RegisterCampaignResponse { get; set; }
    }
}
