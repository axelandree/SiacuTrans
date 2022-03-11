using System;
using Claro.Data;
using System.Runtime.Serialization;
namespace Claro.SIACU.Entity.Transac.Service.Fixed
{
    public class Campaign
    {
        [DataMember]
        public string IDCAMPAIGN { get; set; }
        [DataMember]
        public string DESCRIPTION { get; set; }
        [DataMember]
        public DateTime DATE_END { get; set; }
        [DataMember]
        public int ACTIVE { get; set; }

    }
}
