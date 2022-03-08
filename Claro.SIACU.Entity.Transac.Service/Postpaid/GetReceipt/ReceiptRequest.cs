using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt
{
    [DataContract(Name = "ReceiptRequestTransactions")]
    public class ReceiptRequest : Claro.Entity.Request
    {
        [DataMember]
        public string CustomerCode { get; set; }
        [DataMember]
        public string InvoiceNumber { get; set; }
    }
}
