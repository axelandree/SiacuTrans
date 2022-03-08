using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "childUser")]
    public class ChildUser
    {
        [DataMember(Name = "childUserID")]
        public string childUserID { get; set; }

        [DataMember(Name = "providerUserID")]
        public string providerUserID { get; set; }

        [DataMember(Name = "subProductID")]
        public string subProductID { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }
    }
}
