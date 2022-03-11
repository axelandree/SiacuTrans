using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterActiDesaBonoDesc
{
    [DataContract]
    public class MessageRequest 
    {
        [DataMember(Name = "Header")]
        public HeadersRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public BodyRequest Body { get; set; }
    }
}
