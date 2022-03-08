using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.DiscardHelper;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Fixed
{
    public class DiscardModel
    {
        public List<HELPER_ITEM.GroupHelper> listaGrupos { get; set; }
        public List<HELPER_ITEM.DiscardHelper> listaDescartes { get; set; }
    }
}