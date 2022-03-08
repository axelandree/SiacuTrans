using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterBonoSpeed
{
    [DataContract]
    public class GetRegisterBonoSpeedResponse
    {
        [DataMember(Name = "MessageResponse")]
        public GetRegisterBonoSpeedMessageResponse RegisterBonoSpeedMessageResponse { get; set; }
    }
}
