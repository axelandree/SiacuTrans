using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid
{
    public class RatePlan
    {
        
        [DataMember]
        [Data.DbColumn("PLNV_CODIGO")]
        public string PLNV_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("PLNV_DESCRIPCION")]
        public string PLNV_DESCRIPCION { get; set; }
        [DataMember]
        [Data.DbColumn("PLNN_CARGO_FIJO")]
        public decimal PLNN_CARGO_FIJO { get; set; }
        [DataMember]
        [Data.DbColumn("PLNV_CODIGO_BSCS")]
        public string PLNV_CODIGO_BSCS { get; set; }
        [DataMember]
        [Data.DbColumn("PRDC_CODIGO")]
        public string PRDC_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("PRDV_DESCRIPCION")]
        public string PRDV_DESCRIPCION { get; set; }
        [DataMember]
        [Data.DbColumn("PLNC_FAMILIA")]
        public string PLNC_FAMILIA { get; set; }
        [DataMember]
        [Data.DbColumn("TPROC_CODIGO")]
        public string TPROC_CODIGO { get; set; }
        [DataMember]
        [Data.DbColumn("TPROV_DESCRIPCION")]
        public string TPROV_DESCRIPCION { get; set; }
    }
}
