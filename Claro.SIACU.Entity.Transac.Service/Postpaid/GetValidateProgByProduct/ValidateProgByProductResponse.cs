using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetValidateProgByProduct
{
    [DataContract(Name = "ValidateProgByProductResponseTransactions")]
    public class ValidateProgByProductResponse
    {
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string ErrorCode { get; set; }
        [DataMember]
        public string ErrorMensagge { get; set; }
    }
}
