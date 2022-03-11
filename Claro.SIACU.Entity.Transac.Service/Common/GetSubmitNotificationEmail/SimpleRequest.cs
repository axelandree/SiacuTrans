using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class SimpleRequest
    {
        [DataMember(Name = "Content")]
        public string Content { get; set; }
        [DataMember(Name = "Attachments")]
        public string Attachments { get; set; }
        [DataMember(Name = "Recipients")]
        public RecipientsRequest RecipientsRequest { get; set; }
    }
}
