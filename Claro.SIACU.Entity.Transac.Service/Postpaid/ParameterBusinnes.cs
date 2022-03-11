using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [DataContract(Name = "ParameterBusinnes")]
    public class ParameterBusinnes
    {
        public ParameterBusinnes()
        {}

        [DataMember]
        public string strCode { get; set; }
        [DataMember]
        public string strDescription { get; set; }
        [DataMember]
        public string strCode2 { get; set; }
        [DataMember]
        public string strCode3 { get; set; }
        [DataMember]
        public string strDescription2 { get; set; }
        [DataMember]
        public string strNumber { get; set; }
        [DataMember]
        public string strState { get; set; }
        [DataMember]
        public string strDate { get; set; }
        [DataMember]
        public int intCodeTypeService { get; set; }
        [DataMember]
        public string strIdMotive { get; set; }
    }
}
