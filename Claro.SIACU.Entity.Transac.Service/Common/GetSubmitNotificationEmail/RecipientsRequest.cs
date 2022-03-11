using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class RecipientsRequest
    {
        [DataMember(Name = "Contact")]
        public string Contact { get; set; }
        [DataMember(Name = "ContactGroup")]
        public string ContactGroup { get; set; }
        [DataMember(Name = "Recipient")]
        public string Recipient { get; set; }
        [DataMember(Name = "FileURL")]
        public string FileURL { get; set; }
    }
}
