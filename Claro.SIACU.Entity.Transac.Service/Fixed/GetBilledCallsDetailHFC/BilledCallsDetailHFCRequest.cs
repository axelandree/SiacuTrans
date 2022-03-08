using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetBilledCallsDetailHFC
{
    [DataContract]
    public class BilledCallsDetailHFCRequest : Claro.Entity.Request
    {
        [DataMember]
        public string strInvoiceNumber { get; set; }
        [DataMember]
        public string strTelephone { get; set; }
        [DataMember]
        public string strYearMonth { get; set; }
        [DataMember]
        public string strStarDate { get; set; }
        [DataMember]
        public string strEndDate { get; set; }
        [DataMember]
        public string strSecurity { get; set; }
        [DataMember]
        public string strTypeDb { get; set; }
    }
}
