using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class ExternalInternalTransferModel
    {
        public HELPER_ITEM.CommonServices.GenericItem ItemGeneric { get; set; }
        public List<HELPER_ITEM.CommonServices.GenericItem> ListGeneric { get; set; }
        public List<HELPER_ITEM.HFC.ExternalInternalTransfer.DataProducts> ListDataProducts { get; set; }
    }
}