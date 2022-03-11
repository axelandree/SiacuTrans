using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateBagShare
{
    [DataContract(Name = "ValidateBagShareRequestTransactions")]
    public class ValidateBagShareRequest : Claro.Entity.Request
    {
        [DataMember]
        public string Contrato { get; set; }
    }
}
