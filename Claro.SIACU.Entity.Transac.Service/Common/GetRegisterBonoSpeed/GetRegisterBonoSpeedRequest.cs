using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterBonoSpeed
{
    [DataContract]
    public class GetRegisterBonoSpeedRequest : Claro.Entity.Request
    {
        [DataMember(Name = "MessageRequest")]
        public GetRegisterBonoSpeedMessageRequest RegisterBonoSpeedMessageRequest { get; set; }
    }
}
