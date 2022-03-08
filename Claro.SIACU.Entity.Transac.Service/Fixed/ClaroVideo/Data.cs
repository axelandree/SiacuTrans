using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
     [DataContract(Name = "data")]
    public class Data
    {
        [DataMember(Name = "key")]
         public string key { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }
      
    }
}
