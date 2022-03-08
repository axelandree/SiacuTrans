using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Claro.SIACU.Entity.Transac.Service.Coliving.GetDataBilling
{
    [DataContract]
    public class GetAccountBillingResponse
    {
        [DataMember(Name = "billingAccountId")]
        public string billing_account_id { get; set; }
        [DataMember(Name = "billingAccountCode")]
        public string billing_account_code { get; set; }
        [DataMember(Name = "billingAccountName")]
        public string billing_account_name { get; set; }
        [DataMember(Name = "bmId")]
        public string bm_id { get; set; }
        [DataMember(Name = "csId")]
        public string cs_id { get; set; }
        [DataMember(Name = "csIdPub")]
        public string cs_id_pub { get; set; }
        [DataMember(Name = "currentBalance")]
        public string current_balance { get; set; }
        [DataMember(Name = "dunningFlag")]
        public string dunning_flag { get; set; }
        [DataMember(Name = "invoiceTypeDescription")]
        public string invoice_type_description { get; set; }
        [DataMember(Name = "invoiceTypeId")]
        public string invoice_type_id { get; set; }
        [DataMember(Name = "invoiceTypeIdPub")]
        public string invoice_type_id_pub { get; set; }
        [DataMember(Name = "invoicingInd")]
        public string invoicing_ind { get; set; }
        [DataMember(Name = "lastBilledDate")]
        public string last_billed_date { get; set; }
        [DataMember(Name = "previousBalance")]
        public string previous_balance { get; set; }
        [DataMember(Name = "primaryDocumentCurrencyId")]
        public string primary_document_currency_id { get; set; }
        [DataMember(Name = "primaryFlag")]
        public string primary_flag { get; set; }
        [DataMember(Name = "splitBillingFlag")]
        public string split_billing_flag { get; set; }
        [DataMember(Name = "status")]
        public string status { get; set; }
    }
}
