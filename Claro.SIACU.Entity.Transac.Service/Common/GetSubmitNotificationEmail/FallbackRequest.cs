using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class FallbackRequest
    {
        [DataMember(Name = "FallbackType")]
        public string FallbackType { get; set; }
        [DataMember(Name = "SenderAddress")]
        public string SenderAddress { get; set; }
        [DataMember(Name = "Text")]
        public string Text { get; set; }
    }
}
