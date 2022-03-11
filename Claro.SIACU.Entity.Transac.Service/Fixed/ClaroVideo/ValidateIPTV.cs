using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
     [DataContract(Name="ValidateIPTV")]
    public class ValidateIPTV
    {
        [DataMember]
        [Claro.Data.DbColumn("VALIDACION")]
        public string VALIDACION { get; set; }
    
    }
}
