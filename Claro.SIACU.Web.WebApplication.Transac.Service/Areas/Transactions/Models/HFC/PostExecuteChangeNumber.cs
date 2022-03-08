using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class PostExecuteChangeNumber
    {
        public string IDSESSION { get; set; }
        public string CLASIF_RED { get; set; }
        public string CONTRACT_ID { get; set; }
        public string CUSTOMER_ID { get; set; }
        public string CUSTOMER_TYPE { get; set; }
        public string TYPE_NUMBER { get; set; }
        public string FLAG_FIDEL_TRANS { get; set; }
        public string COST_TRANS { get; set; }
        public string COST_TRANS_IGV { get; set; }
        public string FLAG_LOCU { get; set; }
        public string FLAG_FIDEL_LOCU { get; set; }
        public string COST_LOCU { get; set; }
        public string COST_LOCU_IGV { get; set; }
        public string FLAG_SEND_EMAIL { get; set; }
        public string EMAIL { get; set; }
        public string NATIONAL_CODE { get; set; }
        public string NUMBER_PHONES { get; set; }
        public string TYPE_PHONE{ get; set; }
        public string NRO_TELEF { get; set; }
        public string HLR_CODE { get; set; }
        public string FLAG_PLAN_TIPI { get; set; }
        public string POINT_ATTENTION{ get; set; }
        public string NOTES { get; set; }
        public string CONTACT { get; set; }
        public string FULL_NAME { get; set; }
        public string DOCUMENT { get; set; }
    }
}