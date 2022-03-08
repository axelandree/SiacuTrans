using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Common.GetTypification
{
    [DataContract(Name = "TypificationRequest")]
    public class TypificationRequest : Claro.Entity.Request
    {
        [DataMember]
        public string TRANSACTION_NAME { get; set; }
    }
}
