using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetCallDetailInputFixed
{
    [DataContract]
    public class CallDetailInputFixedRequest
    {
        [DataMember]
        public Entity.Transac.Service.Fixed.GetBpelCallDetail.BpelCallDetailRequest objRequest { get; set; }
    }
}
