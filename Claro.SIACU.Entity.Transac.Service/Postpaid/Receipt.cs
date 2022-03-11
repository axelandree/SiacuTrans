using System.Runtime.Serialization;
    
namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    [Data.DbTable("TEMPO")]
    [DataContract(Name = "ReceiptPostPaid")]
    public class Receipt
    {
        public Receipt()
        {
            RECIBO_DETALLADO = new DetailReceipt();
        }

        [DataMember]
        [Data.DbColumn("InvoiceNumber")]
        public string NUMERO_RECIBO { get; set; }

        [DataMember]
        [Data.DbColumn("FechaEmision")]
        public string FECHA_EMISION { get; set; }

        [DataMember]
        [Data.DbColumn("FechaVencimiento")]
        public string FECHA_VENCIMIENTO { get; set; }

        [DataMember]
        [Data.DbColumn("periodo")]
        public string PERIODO { get; set; }

        [DataMember]
        public string INVOICENUMBER { get; set; }

        [DataMember]
        public bool ENVIO_CORREO { get; set; }

        [DataMember]
        public DetailReceipt RECIBO_DETALLADO { get; set; }
        [DataMember]
        [Data.DbColumn("InvoiceNumber")]
        public string INVOICE_NUM { get; set; }
    }
}