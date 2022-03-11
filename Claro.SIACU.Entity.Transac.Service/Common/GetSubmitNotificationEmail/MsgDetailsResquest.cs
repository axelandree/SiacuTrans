using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class MsgDetailsResquest
    {
        [DataMember(Name = "Subject")]
        public string Subject { get; set; }
        [DataMember(Name = "Simple")]
        public SimpleRequest SimpleRequest  { get; set; }
        [DataMember(Name = "Template")]
        public TemplateRequest TemplateRequest { get; set; }
        [DataMember(Name = "Fallback")]
        public FallbackRequest FallbackRequest { get; set; }
        [DataMember(Name = "ReadReply")]
        public bool ReadReply { get; set; }
        [DataMember(Name = "DeliveryReport")]
        public bool DeliveryReport { get; set; }
    }
}
