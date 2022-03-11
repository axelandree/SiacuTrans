using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetTechnicalVisitResult
{
    [DataContract(Name = "TechnicalVisitResultRequest")]
    public class TechnicalVisitResultRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strCoId { get; set; }
        [DataMember]
        public string strCustomerId { get; set; }
        [DataMember]
        public string strTmCode { get; set; }
        [DataMember]
        public string strCodPlanSisact { get; set; }
        [DataMember]
        public string strTrama { get; set; }
    }
}
