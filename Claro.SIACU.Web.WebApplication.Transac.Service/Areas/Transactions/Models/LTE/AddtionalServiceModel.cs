using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE
{
    public class AddtionalServiceModel
    {
        public string StrTransaccionId { get; set; }
        public string StrNameAplication { get; set; }
        public string StrIpAplication { get; set; }
        public string _StrMsisdn { get; set; }
        public string StrCoId { get; set; }
        public string StrCoSer { get; set; }
        public int IntFlagOccPenalty { get; set; }
        public double DblPenaltyAmount { get; set; }
        public double DblNewCf { get; set; }
        public string StrTypeRegistry { get; set; }
        public string StrCycleFacturation { get; set; }
        public string StrCodeSer { get; set; }
        public string StrDescriptioCoSer { get; set; }
        public string StrNroAccount { get; set; }
        public string StrDateProgramation { get; set; }
        public string StrInteractionId { get; set; }
        public string StrTypeSerivice { get; set; }
        public string StrCodeResult { get; set; }
        public string StrResult { get; set; }
        public string StrMessage { get; set; }
        public bool BlValues { get; set; }
        public string TypeTransaction { get; set; }
        public bool ChkEnviarPorEmail { get; set; }
        public int StrCaseId { get; set; }
        public string StrIpCustomer { get; set; }
        public string StrCacDac { get; set; }
        public string StrNote { get; set; }
        public int StrStateService { get; set; }
        public CustomersDataModel oCustomersDataModel { get; set; }
        public ServerModel oServerModel { get; set; }
        public LineDataModel oLineDataModel { get; set; }
        public HiddenModel oHiddenModel { get; set; }


    }
    public class ServerModel
    {
        public ServerModel() { }
        public string StrAccountUser { get; set; }
        public string StrIdSession { get; set; }
        public string StrNameServer { get; set; }
        public string StrIpServer { get; set; }


    }

    public class HiddenModel
    {
        public HiddenModel()
        {
        }

        public string HdnCostoPvuSel { get; set; }
        public string HdnDesCoSerSel { get; set; }
        public string HdnCostoBscs { get; set; }
        public string HdnTipoTransaccion { get; set; }
        public string HdnCargoFijoSel { get; set; }
        public string HdnCoSerSel { get; set; }
        public string TxtNota { get; set; }
        public string PvwNumeroTelefono { get; set; }
        public string Plan { get; set; }
        public string TxtEnviarporEmail { get; set; }
        public bool ChkEnviarPorEmail { get; set; }
        public bool ChkProgramar { get; set; }
        public string HdnFechaProg { get; set; }
        public string HdnClase { get; set; }
        public string HdnSubClassCode { get; set; }
        public string HdnSubClass { get; set; }
        public string HdnClaseCode { get; set; }
        public string HdnInteractionCode { get; set; }
        public string HdnTypeCode { get; set; }
        public string HdnType { get; set; }
        public string StrTypeProduct { get; set; }
    }
}