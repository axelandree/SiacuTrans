using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "extraData")]
    public class ExtraData
    {
        [DataMember(Name = "data")]
        public List<Data> data { get; set; }

    }
}
