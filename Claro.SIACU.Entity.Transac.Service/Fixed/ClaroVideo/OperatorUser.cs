using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
    [DataContract(Name = "operatorUser")]
    public class OperatorUser
    {
        [DataMember(Name = "operatorUserID")]
        public string operatorUserID { get; set; }

        [DataMember(Name = "providerUserID")]
        public string providerUserID { get; set; }

        [DataMember(Name = "subProductID")]
        public string subProductID { get; set; }

        [DataMember(Name = "description")]
        public string description { get; set; }



    }
}
