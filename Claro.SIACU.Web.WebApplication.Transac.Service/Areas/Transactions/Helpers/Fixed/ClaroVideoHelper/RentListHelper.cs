using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper.ResponseGrid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.ClaroVideoHelper
{
    public class RentListHelper
    {
      
        public List<RentHelper> rent { get; set; }
        public List<HELPER_ITEM.ListRentalUser> ListRentalUser { get; set; }
    }
}