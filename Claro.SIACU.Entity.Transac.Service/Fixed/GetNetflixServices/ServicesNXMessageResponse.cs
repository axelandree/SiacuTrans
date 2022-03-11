using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetNetflixServices
{
    [DataContract]
    public class ServicesNXMessageResponse
    {
        [DataMember(Name = "Header")]
        public ServicesNXHeaderResponse Header { get; set; }
        [DataMember(Name = "Body")]
        public ServicesNXBodyResponse Body { get; set; }
    }
}