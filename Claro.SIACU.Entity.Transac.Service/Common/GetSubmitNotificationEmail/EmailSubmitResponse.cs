using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract(Name = "EmailSubmitResponse")]
    public class EmailSubmitResponse
    {
        [DataMember(Name = "CampaignID")]
        public string CampaignID { get; set; }
        [DataMember(Name = "jobCost")]
        public double jobCost { get; set; }
        [DataMember(Name = "RecipientsCount")]
        public string RecipientsCount { get; set; }
        [DataMember(Name = "Status")]
        public StatusResponse StatusResponse { get; set; }
    }
}
