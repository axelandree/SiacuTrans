using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCamapaign
{
    [DataContract]
    public class CamapaignResponse
    {
        [DataMember]
        public List<GenericItem> LstCampaign { get; set; }

        [DataMember]
        public List<Campaign> lstCampaigns { get; set; }
    }
}
