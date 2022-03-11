using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Postpaid.GetBillPostDetail
{
    [DataContract(Name = "BillPostDetailRequestTransactions")]
    public class BillPostDetailRequest : Claro.Entity.Request
    {
        [DataMember]
        public string vMSISDN { get; set; }
        [DataMember]
        public string vFECHA_INI { get; set; }
        [DataMember]
        public string vFECHA_FIN { get; set; }
        [DataMember]
        public string vFLAG_NACIONAL { get; set; }
        [DataMember]
        public string vFLAG_SMS_MMS { get; set; }
        [DataMember]
        public string vFLAG_GPRS { get; set; }
        [DataMember]
        public string vFLAG_INTERNACIONA { get; set; }
        [DataMember]
        public string vFLAG_777 { get; set; }
        [DataMember]
        public string vFLAG_VAS { get; set; }
        [DataMember]
        public string vFLAG_DETALLE { get; set; }
        [DataMember]
        public string vFLAG_TIPO_VISUAL { get; set; }
        [DataMember]
        public string vFlag { get; set; }
        [DataMember]
        public string vSeguridad { get; set; }
    }
}
