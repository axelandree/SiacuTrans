namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE
{
    public class AddServiceAditionalModel
    {
       
        public string StrTransaccionId { get; set; }
        public string StrNameAplication { get; set; }
        public string StrIpAplication { get; set; }
        public string StrMsisdn { get; set; }
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

       // public CustomersDataModel oCustomersDataModel { get; set; }
        //public ServerModel oServerModel { get; set; }

      //  public LineDataModel oLineDataModel { get; set; }
        //public HiddenModel oHiddenModel { get; set; }

        public AddServiceAditionalModel()
        {
            //this.oHiddenModel = new HiddenModel();
          //  this.oLineDataModel = new LineDataModel();
            //this.oCustomersDataModel = new CustomersDataModel();
            //this.oServerModel = new ServerModel();
            this.StrTransaccionId  = string.Empty;
            this.StrNameAplication = string.Empty;
            this.StrIpAplication = string.Empty;
            this.StrMsisdn = string.Empty;
            this.StrCoId = string.Empty;
            this.StrCoSer = string.Empty;
            this.IntFlagOccPenalty = 0;
            this.DblPenaltyAmount =0;
            this.DblNewCf = 0;
            this.StrTypeRegistry = string.Empty;
            this.StrCycleFacturation = string.Empty;
            this.StrCodeSer = string.Empty;
            this.StrDescriptioCoSer = string.Empty;
            this.StrNroAccount = string.Empty;
            this.StrDateProgramation = string.Empty;
            this.StrInteractionId = string.Empty;
            this.StrTypeSerivice = string.Empty;
            this.StrCodeResult = string.Empty;
            this.StrResult = string.Empty;
            this.StrMessage = string.Empty;
            this.BlValues = false;
            this.TypeTransaction = string.Empty;
            this.ChkEnviarPorEmail = false;
            this.StrCaseId = 0;
            this.StrIpCustomer = string.Empty;
            this.StrCacDac = string.Empty;
            this.StrNote = string.Empty;
        }
    }

    //public class ServerModel
    //{
    //    public ServerModel() { }
    //    public string StrAccountUser { get; set; }
    //    public string StrIdSession { get; set; }
    //    public string StrNameServer { get; set; }
    //    public string StrIpServer { get; set; }

    //    //public ServerModel()
    //    //{
    //    //    this.StrAccountUser = string.Empty;
    //    //    this.StrIdSession = string.Empty;
    //    //    this.StrNameServer = string.Empty;
    //    //    this.StrIpServer = string.Empty;
    //    //}
    //}
    //public class HiddenModel
    //{
    //    public HiddenModel() { }
    //    public string HdnCostoPvuSel { get; set; }
    //    public string HdnDesCoSerSel { get; set; }
    //    public string HdnCostoBscs { get; set; }
    //    public string HdnTipoTransaccion { get; set; }
    //    public string HdnCargoFijoSel { get; set; }
    //    public string HdnCoSerSel { get; set; }
    //    public string TxtNota { get; set; }
    //    public string PvwNumeroTelefono { get; set; }
    //    public string Plan { get; set; }
    //    public string TxtEnviarporEmail { get; set; }
    //    public bool ChkEnviarPorEmail { get; set; }
    //    public bool ChkProgramar { get; set; }
    //    public string HdnFechaProg { get; set; }
    //    public string HdnClaseDescription { get; set; }
    //    public string HdnSubClassCode { get; set; }
    //}
}