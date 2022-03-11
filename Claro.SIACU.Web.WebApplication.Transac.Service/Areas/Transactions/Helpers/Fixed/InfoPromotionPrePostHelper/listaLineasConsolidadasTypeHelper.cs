using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HELPER_ITEM = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper
{
    public class listaLineasConsolidadasTypeHelper
    {
        public List<HELPER_ITEM.lineaConsolidadaHelper> lineaConsolidada { get; set; }

        public listaLineasConsolidadasTypeHelper()
        {
            lineaConsolidada = new List<lineaConsolidadaHelper>();
        }

    }
}