﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class CreateUserOttResponseHelper
    {
        public string userId { get; set; }
        public string resultCode { get; set; }
        public string resultMessage { get; set; }
        public string correlatorId { get; set; }
        public string countryId { get; set; }
        public string serviceName { get; set; }
        public string providerId { get; set; }
        public List<ExtensionInfoHelper> extensionInfo { get; set; }
        public string CUSTOMERID { get; set; }
    }
}