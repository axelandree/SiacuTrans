using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class QueryOttResponseHelper
    {
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
        public string correlatorId { get; set; }
        public PackageHelper packages { get; set; }
        public RentListHelper rentList { get; set; }
        public VisualizationsListHelper visualizationsList { get; set; }
        public DeviceListHelper deviceList { get; set; }
        public PackagesListHelper packagesList { get; set; }
        public PaymentMethodListHelper paymentMethodList { get; set; }
        public SubscriptionsHelper subscriptions { get; set; }
        public EventListHelper eventList { get; set; }
        public string countryId { get; set; }
        public string serviceName { get; set; }
        public string providerId { get; set; }
        public List<ExtensionInfoHelper> extensionInfo { get; set; }


      

    }
}