using System.Collections.Generic;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails;
using Claro.Helpers.Transac.Service;
using Org.BouncyCastle.Crypto;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.CallDetails
{
    public class BilledCallsDetailHfcModel : IExcel
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
        public string ContractId { get; set; }
        public string CodePlanInst { get; set; }
        public string CurrentUser { get; set; }
        public string Transaction { get; set; }
        public string Note { get; set; }
        public string CodeTipification { get; set; }
        public string MonthEmision { get; set; }
        public string YearEmision { get; set; }
        public string CacDac { get; set; }
        public string Sn { get; set; }
        public string IpServidor { get; set; }
        public string IdSession { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public string InvoceNumber { get; set; }
        public string NroDoc { get; set; }
        public string LastName { get; set; }
        public string RepresentLegal { get; set; }
        [Header(Title = "StardDate")]
        public string StardDate { get; set; }
        [Header(Title = "EndDate")]
        public string EndDate { get; set; }
        public string CodePerfil { get; set; }
        public string NameComplet { get; set; }
        public string TypeClient { get; set; }
        public string RazonSocial { get; set; }
        public string Periodo { get; set; }
        public string TypeDoc { get; set; }
        public string localad { get; set; }
        public string DescCacDac { get; set; }
        public string LocalAdd { get; set; }
        public string product { get; set; }
        public string StrDistrict { get; set; }
        public string StrDepartament { get; set; }
        public string StrProvince { get; set; }
        public string StrModality { get; set; }
        public string StrResponseCode { get; set; }
        public string StrResponseMessage { get; set; }
        public BpelCallDetailModel BpelCallDetailModel { get; set; }

        [Header(Title = "ListExportExcel")]
        public List<BilledCallsDetail> ListExportExcel { get; set; }

        public string FechaCicloIni { get; set; }
        public string FechaCicloFin { get; set; }
    }
}