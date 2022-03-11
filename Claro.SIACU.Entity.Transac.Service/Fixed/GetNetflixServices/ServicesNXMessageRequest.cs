using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices
{
    [DataContract]
    public class ServicesNXMessageRequest
    {
        [DataMember(Name = "Header")]
        public ServicesNXHeaderRequest Header { get; set; }
        [DataMember(Name = "Body")]
        public ServicesNXBodyRequest Body { get; set; }
    }
}
