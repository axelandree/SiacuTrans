
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class ProvisionSubscriptionHelper
    {
        public ResultHelper result { get; set; }


        public string hubTransID { get; set; }


        public string operatorUserID { get; set; }


        public string childuserId { get; set; }


        public string providerUserID { get; set; }


        public List<ExtensionInfoHelper> extensionInfo { get; set; }
    }
}