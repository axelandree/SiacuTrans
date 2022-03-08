using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class CheckDevicesModel
    {
        public string StrIdSession { get; set; }
        public string StrCoId { get; set; }
        public string StrMsisdn { get; set; }
        public string StrCountItems { get; set; }
        public List<ServicesDTH> ListServicesDTH { get; set; }
    }
}