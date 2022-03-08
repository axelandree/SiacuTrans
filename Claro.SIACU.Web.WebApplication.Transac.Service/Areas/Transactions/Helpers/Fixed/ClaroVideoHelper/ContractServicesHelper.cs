using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class ContractServicesHelper
    {
        public string GroupCode { get; set; }
        public string GroupDescription { get; set; }
        public string GroupPos { get; set; }
        public string ServiceCode { get; set; }
        public string ServiceDescription { get; set; }
        public string ServicePos { get; set; }
        public string ExclusiveCode { get; set; }
        public string ExclusiveDescription { get; set; }
        public string State { get; set; }
        public string ValidityDate { get; set; }
        public string SubscriptionFeeAmount { get; set; }
        public string AmountFixedCharge { get; set; }
        public string ModifiedShare { get; set; }
        public string FinalAmount { get; set; }
        public string ValidPeriods { get; set; }
        public string DisableLock { get; set; }
        public string ActiveBlocking { get; set; }
    }
}