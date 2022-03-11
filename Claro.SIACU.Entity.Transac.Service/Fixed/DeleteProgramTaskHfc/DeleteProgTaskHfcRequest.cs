using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Fixed.DeleteProgramTaskHfc
{
    [DataContract]
    public class DeleteProgTaskHfcRequest : Claro.Entity.Request
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
        public string VstrServCEstado { get; set; }
    }
}
