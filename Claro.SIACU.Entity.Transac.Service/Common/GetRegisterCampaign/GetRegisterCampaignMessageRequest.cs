using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterCampaign
{
    [DataContract]
    public class GetRegisterCampaignMessageRequest
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }
        [DataMember(Name="Body")]
        public GetRegisterCampaignBodyRequest RegisterCampaignBodyRequest { get; set; } 
    }
}
