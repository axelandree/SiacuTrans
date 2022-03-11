using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class QueryUserOttResponseHelper
    {
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
        public string correlatorId { get; set; }
        public UserDataHelper userData { get; set; }
        public SubscriptionListHelper subscriptionList { get; set; }
        public string countryId { get; set; }
        public string serviceName { get; set; }
        public string providerId { get; set; }
        public List<ExtensionInfoHelper> extensionInfo { get; set; }
        public List<HELPER_ITEM.ListUserSubscription> ListUserSubscription { get; set; }
        public string CUSTOMERID { get; set; }
    }
}