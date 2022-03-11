using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name="TarifePlan")]
    public class TarifePlan
    {
        [DataMember]
        [Data.DbColumn("strPLNVCode")]
        public string strPLNVCode { get; set; }
        [DataMember]
        [Data.DbColumn("strPLNVDescription")]
        public string strPLNVDescription { get; set; }
        [DataMember]
        [Data.DbColumn("dcmPLNNCargoFijo")]
        public decimal dcmPLNNCargoFijo { get; set; }
        [DataMember]
        [Data.DbColumn("strPLNCodeBSCS")]
        public string strPLNCodeBSCS { get; set; }
        [DataMember]
        [Data.DbColumn("strPRDCCode")]
        public string strPRDCCode { get; set; }
        [DataMember]
        [Data.DbColumn("strPRDVDescription")]
        public string strPRDVDescription { get; set; }
        [DataMember]
        [Data.DbColumn("strPlNCFamally")]
        public string strPlNCFamally { get; set; }
        [DataMember]
        [Data.DbColumn("strTPROCCode")]
        public string strTPROCCode { get; set; }
        [DataMember]
        [Data.DbColumn("strTPROVDescription")]
        public string strTPROVDescription { get; set; }
    }
}
