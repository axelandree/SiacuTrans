using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateProgByProduct
{
    [DataContract(Name = "ValidateProgByProductRequestTransactions")]
    public class ValidateProgByProductRequest : Claro.Entity.Request
    {
        
        [DataMember]
        public string Cuenta { get; set; }
        [DataMember]
        public string ArrayCodProd { get; set; }
    }
}
