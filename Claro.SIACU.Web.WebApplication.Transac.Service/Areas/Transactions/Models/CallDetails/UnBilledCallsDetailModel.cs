using System.Collections.Generic;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails;
using Claro.Helpers.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.CallDetails
{
    public class UnBilledCallsDetailHfcModel : IExcel
    {
        [Header(Title = "ID")]
        public string Id { get; set; }
        [Header(Title = "Customer")]
        public string Customer { get; set; }
        public string Cuenta { get; set; }
        public string Plan { get; set; }
        public string Telephone { get; set; }
        public string Total { get; set; }
        public string TotalSms { get; set; }
        public string TotalMms { get; set; }
        public string TotalGprs { get; set; }
        public string TotalRegistro { get; set; }
        public string CargoFinal { get; set; }
        [Header(Title = "StardDate")]
        public string StardDate { get; set; }
        [Header(Title = "EndDate")]
        public string EndDate { get; set; }
        [Header(Title = "ListExportExcel")]
        public List<UnBilledCallsDetail> ListExportExcel { get; set; }
    }
}