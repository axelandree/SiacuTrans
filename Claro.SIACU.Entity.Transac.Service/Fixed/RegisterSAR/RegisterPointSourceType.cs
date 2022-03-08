using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.RegisterSAR
{
    [DataContract]
    public class RegisterPointSourceType
    {
        [DataMember]
        public string piObjId { get; set; }
        [DataMember]
        public string piRegion { get; set; }
        [DataMember]
        public string piCapPac { get; set; }
        [DataMember]
        public string piUserCreate { get; set; }
        [DataMember]
        public string piUserUpdate { get; set; }
        [DataMember]
        public string piCacDac { get; set; }
    }
}
