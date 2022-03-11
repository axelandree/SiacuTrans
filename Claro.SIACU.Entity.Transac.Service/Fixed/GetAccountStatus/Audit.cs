
using System.Runtime.Serialization;


namespace Claro.SIACU.Entity.Transac.Service.Fixed.GetAccountStatus
{
    [DataContract]
    public class Audit
    {
        [DataMember(Name = "txId")]
        public string txId { get; set; }

        [DataMember(Name = "errorCode")]
        public string errorCode { get; set; }

        [DataMember(Name = "errorMsg")]
        public string errorMsg { get; set; }
    }
}
