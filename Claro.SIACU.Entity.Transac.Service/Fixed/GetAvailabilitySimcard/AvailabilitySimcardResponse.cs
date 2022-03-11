using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAvailabilitySimcard
{
    [DataContract]
    public class AvailabilitySimcardResponse : Claro.Entity.Response
    {
        [DataMember]
        public string ResultCode { get; set; }

        [DataMember]
        public string ResultMessage { get; set; }
    }
}
