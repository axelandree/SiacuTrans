using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.ClaroVideo
{
     [DataContract(Name="ConsultIPTV")]
    public class ConsultIPTV
    {
        [DataMember]
        [Claro.Data.DbColumn("SERVV_CODIGO_EXT")]
         public string SERVV_CODIGO_EXT { get; set; }
        [DataMember]
        [Claro.Data.DbColumn("SERVV_DES_EXT")]
        public string SERVV_DES_EXT { get; set; }
    }
}
