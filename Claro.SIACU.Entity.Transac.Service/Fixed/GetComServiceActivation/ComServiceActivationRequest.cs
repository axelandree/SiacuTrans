using System;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetComServiceActivation
{
    [DataContract]
    public class ComServiceActivationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrIdTransaction { get; set; }
        [DataMember]
        public string StrCodeAplication { get; set; }
        [DataMember]
        public string StrIpAplication { get; set; }
        [DataMember]
        public DateTime StrDateProgramation { get; set; }
        [DataMember]
        public DateTime StrDateRegistre { get; set; }
        [DataMember]
        public int StrFlagSearch { get; set; }
        [DataMember]
        public int StrFlagOccPenalty { get; set; }
        [DataMember]
        public double strPenalty { get; set; }
        [DataMember]
        public double strAmountFIdPenalty { get; set; }
        [DataMember]
        public double strNewCF { get; set; }
         [DataMember]
        public string strBillingCycle { get; set; }
         [DataMember]
         public string strTicklerCode { get; set; }
        [DataMember]
        public string StrCoId { get; set; }
        [DataMember]
        public string StrCodeCustomer { get; set; }
        [DataMember]
        public string StrProIds { get; set; }
        [DataMember]
        public string StrDatRegistry { get; set; }
        [DataMember]
        public string StrUser { get; set; }
        [DataMember]
        public string strInteraction { get; set; }
        [DataMember]
        public string StrTelephone { get; set; }
        [DataMember]
        public string StrTypeService { get; set; }
        [DataMember]
        public string StrCoSer { get; set; }
        [DataMember]
        public string StrTypeRegistry { get; set; }
        [DataMember]
        public string StrUserSystem { get; set; }
        [DataMember]
        public string StrUserApp { get; set; }
        [DataMember]
        public string StrEmailUserApp { get; set; }
        [DataMember]
        public string StrDesCoSer { get; set; }
        [DataMember]
        public string StrCodeInteraction { get; set; }
        [DataMember]
        public string StrNroAccount { get; set; }
        [DataMember]
        public string StrCost { get; set; }
        [DataMember]
        public string StrMessage { get; set; }
        [DataMember]
        public string StrCodeBell { get; set; }

 
    }
}
