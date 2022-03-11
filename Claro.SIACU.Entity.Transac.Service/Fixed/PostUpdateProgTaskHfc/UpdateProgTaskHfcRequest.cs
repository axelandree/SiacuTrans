using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.PostUpdateProgTaskHfc
{
    [DataContract]
    public class UpdateProgTaskHfcRequest : Claro.Entity.Request
    {
        [DataMember]
        public string StrIdSession { get; set; }
        [DataMember]
        public string StrTransaction { get; set; }
        [DataMember]
        public string VstrServCod { get; set; }
        [DataMember]
        public string VstrCodId { get; set; }
        [DataMember]
        public string VstrCustomerId { get; set; }
        [DataMember]
        public string VstrServFProg { get; set; }
        [DataMember]
        public string VstrServIdBat { get; set; }
        [DataMember]
        public string VstrServFEjec { get; set; }
        [DataMember]
        public string VstrServCEstado { get; set; }
        [DataMember]
        public string VstrServMenErr { get; set; }
        [DataMember]
        public string VstrServCodErr { get; set; }
    }
}
