using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract(Name = "EmailSubmitRequest")]
    public class EmailSubmitRequest : Claro.Entity.Request
    {
        [DataMember(Name = "Header")]
        public Common.GetDataPower.HeadersRequest Header { get; set; }

        [DataMember(Name = "CampaignName")]
        public string CampaignName {get;set;}
        [DataMember(Name = "CampaignDesc")]
        public string CampaignDesc {get;set;}
        [DataMember(Name = "CampaignCategory")]
        public string CampaignCategory {get;set;}
        [DataMember(Name = "PromotionalCategory")]
        public string PromotionalCategory {get;set;}
        [DataMember(Name = "CallBackURL")]
        public string CallBackURL {get;set;}
        [DataMember(Name = "ScheduledDateTime")]
        public string ScheduledDateTime {get;set;}
        [DataMember(Name = "ExpiryDateTime")]
        public string ExpiryDateTime { get; set; }
        [DataMember(Name = "Sender")]
        public SenderRequest SenderRequest { get; set; }
        [DataMember(Name = "MsgDetails")]
        public MsgDetailsResquest MsgDetailsResquest { get; set; }
    }
}
