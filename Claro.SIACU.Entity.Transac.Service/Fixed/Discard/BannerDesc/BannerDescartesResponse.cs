using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.Discard.BannerDesc
{
    [DataContract]
    public class BannerDescartesResponse
    {
        [DataMember]
        public ResponseAudi responseAudit { get; set; }
        [DataMember]
        public ResponseDataConsult responseData { get; set; }
    }
}
