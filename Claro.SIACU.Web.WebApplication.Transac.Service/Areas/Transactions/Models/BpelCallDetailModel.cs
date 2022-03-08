namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class BpelCallDetailModel
    {
        public HeaderRequestTypeBpelModel HeaderRequestTypeBpelModel { get; set; }
        public DetailCallRequestBpelModel DetailCallRequestBpelModel { get; set; }
        public string StrSecurity { get; set; }
        public string StrIdSession { get; set; }
        public string StrTransaction { get; set; }
        public string StrTelephone { get; set; }
        public string StrMonthEmision { get; set; }
        public string StrYearEmision { get; set; }
        public string StrCodeTipification { get; set; }
        public string StrHdnType { get; set; }
        public string StrHdnClase { get; set; }
        public string StrHdnSubClass { get; set; }
    }
}