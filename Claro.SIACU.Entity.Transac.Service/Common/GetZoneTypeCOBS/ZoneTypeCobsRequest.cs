using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetZoneTypeCOBS
{
     [DataContract(Name = "ZoneTypeCobsRequestCommon")]
     public  class ZoneTypeCobsRequest:  Claro.Entity.Request
    {

        [DataMember]
        public string CodZoneType { get; set; }

    }
}
