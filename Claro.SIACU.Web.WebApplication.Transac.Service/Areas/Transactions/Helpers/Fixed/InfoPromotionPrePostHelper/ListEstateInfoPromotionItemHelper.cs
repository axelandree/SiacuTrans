using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Fixed.InfoPromotionPrePostHelper
{
    public class ListEstateInfoPromotionItemHelper
    {

        public ListEstateInfoPromotionItemHelper() { }

        [Header(Title = "Medio", Order = Claro.Constants.NumberZero)]
        public string Medio { get; set; }

        [Header(Title = "Permitido", Order = Claro.Constants.NumberOne)]
        public string Permitido { get; set; }

        [Header(Title = "No Permitido", Order = Claro.Constants.NumberTwo)]
        public string NoPermitido { get; set; }

        [Header(Title = "Pendiente", Order = Claro.Constants.NumberThree)]
        public string Pendiente { get; set; }

        [Header(Title = "Fecha", Order = Claro.Constants.NumberFour)]
        public string Fecha { get; set; }

        [Header(Title = "Canal", Order = Claro.Constants.NumberFive)]
        public string Canal { get; set; }       
    }
}