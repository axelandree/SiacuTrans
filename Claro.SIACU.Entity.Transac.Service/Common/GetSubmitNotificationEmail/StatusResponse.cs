using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetSubmitNotificationEmail
{
    [DataContract]
    public class StatusResponse
    {
        [DataMember(Name = "StatusCode")]
        public string StatusCode { get; set; }
        [DataMember(Name = "StatusText")]
        public string StatusText { get; set; }
    }
}
