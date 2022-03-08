using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetReceipt
{
    [DataContract(Name = "ReceiptResponseTransactions")]
    public class ReceiptResponse
    {
        [DataMember]
        public Claro.SIACU.Entity.Transac.Service.Postpaid.Receipt ObjReceipt { get; set; }
    }
}

