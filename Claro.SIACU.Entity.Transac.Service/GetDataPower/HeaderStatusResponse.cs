using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.GetDataPower
{
    [DataContract(Name = "status")]
    public class HeaderStatusResponse
    {
        [DataMember(Name = "type")]
        public string type { get; set; }
        [DataMember(Name = "code")]
        public string code { get; set; }
        [DataMember(Name = "message")]
        public string message { get; set; }
        [DataMember(Name = "msgid")]
        public string msgid { get; set; }
    }
}
