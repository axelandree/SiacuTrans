using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Prepaid.BilledOutCallDetail;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Prepaid

{
    public class BilledOutCallDetailsViewModel
    {
        public HELPER_ITEM.BilledOutCallDetails BilledOutCallDetails { get; set; }
        public HELPER_ITEM.BilledCalltypification BilledCalltipificacion { get; set; }
        public List<HELPER_ITEM.BilledOutCallDetails> ListBilledOutCallDetails { get; set; }


    }
}