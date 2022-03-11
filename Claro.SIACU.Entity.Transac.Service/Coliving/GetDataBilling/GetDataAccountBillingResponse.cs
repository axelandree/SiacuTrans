using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetDataAccountBillingResponse
    {
        [DataMember(Name = "balanceGlacode")]
        public string balance_glacode { get; set; }
        [DataMember(Name = "billingAccountCode")]
        public string billing_account_code { get; set; }
        [DataMember(Name = "billingAccountName")]
        public string billing_account_name { get; set; }
        [DataMember(Name = "callDetailsFlag")]
        public string call_details_flag { get; set; }
        [DataMember(Name = "contactSeqno")]
        public string contact_seqno { get; set; }
        [DataMember(Name = "contactSeqnoTemp")]
        public string contact_seqno_temp { get; set; }
        [DataMember(Name = "currentBalance")]
        public string current_balance { get; set; }
        [DataMember(Name = "dunningFlag")]
        public string dunning_flag { get; set; }
        [DataMember(Name = "foreignCurcyLvl")]
        public string foreign_curcy_lvl { get; set; }
        [DataMember(Name = "glcode")]
        public string glcode { get; set; }
        [DataMember(Name = "invoiceTypeId")]
        public string invoice_type_id { get; set; }
     }
}
