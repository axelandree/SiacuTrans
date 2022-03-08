using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetRegisterBonoSpeed
{
    [DataContract]
    public class GetRegisterBonoSpeedBodyRequest
    {
        [DataMember(Name = "bonoId")]
        public string BonoId { get; set; }
        [DataMember(Name = "coId")]
        public string CoId { get; set; }
        [DataMember(Name = "periodo")]
        public string Periodo { get; set; }
    }
}
