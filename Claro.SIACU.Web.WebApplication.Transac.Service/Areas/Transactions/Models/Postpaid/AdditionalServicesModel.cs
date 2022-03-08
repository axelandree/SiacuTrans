using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using Claro.SIACU.Entity.Transac.Service.Common;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;
using Claro.SIACU.Web.WebApplication.Transac.Service.PostTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class AdditionalServicesModel
    {
        public string IdSession { get; set; }
        public string ContractId { get; set; }
        public string ContactCode { get; set; }
        public string HidTransactionDTH { get; set; }
        public string HidTransaction { get; set; }
        public string HidClassId { get; set; }
        public string HidSubClassId { get; set; }
        public string HidType { get; set; }
        public string HidClassDes { get; set; }
        public string HidSubClassDes { get; set; }
        
        public string Transaction { get; set; }
        public string EnableTelephonyDTH { get; set; }
        public string HidNumberPhone { get; set; }
        public string HidCodId { get; set; }
        public string HidListTypeSolRoaming { get; set; }
        public int HidProfileSolRoaming { get; set; }
        public string UserLogin { get; set; }


        public string MessageCode { get; set; }
        public string Message { get; set; }
        public string MessageLabel { get; set; }
        public string DisableSuccess { get; set; }
        public string DisableError { get; set; }


        public List<CommonTransacService.ContractByPhoneNumber> ListContractByPhoneNumber { get; set; }
        public string SessionProfile { get; set; }
        public string HidCodServRoaming { get; set; }
        public string HidMinDateDeactivation { get; set; }
        public string HidMaxDateDeactivacion { get; set; }
        public string HidTotalFixedCharge { get; set; }
        public string HidCodServ4G { get; set; }
        public string HidCodOptAuthorized { get; set; }
        public string HidAccessWC { get; set; }
        public string HidAccessMCP { get; set; }
        public string HidStateUserBSCS { get; set; }
        public string HidClassIdMCP { get; set; }
        public string HidSubClassIdMCP { get; set; }
        public string HidTypeMCP { get; set; }
        public string HidClassDesMCP { get; set; }
        public string HidSubClassDesMCP { get; set; }
        public string TxtNote { get; set; }
        public bool blnValidate { get; set; }
        public string HidNameService { get; set; }
        public string HidCodService { get; set; }
        public string HidFixedCharge { get; set; }
        public string HidFixedChargeM { get; set; }
        public double HidNumberPeriod { get; set; }
        public string HidEstGraMCP { get; set; }
        public string chkSendMail_IsCheched { get; set; }
        public string cboCACDACValue { get; set; }
        public string HidQoutMod { get; set; }
        public string HidPeriodMod { get; set; }
        public string HidCarFixed { get; set; }
        public string HidPeriodAnt { get; set; }
        public string HidQuotAnt { get; set; }
        public string TypeDocument { get; set; }
        public string HidCaseId { get; set; }
        public string HidCodId_Contract { get; set; }
        public string HidSnCode { get; set; }
        public string HidSpCode { get; set; }
        public string HidStateMod { get; set; }
        public string HidProgramingRoamming { get; set; }
        public string HidAction { get; set; }
        public string chkProgram_IsChecked { get; set; }
        public string txtDateApp { get; set; }
        public string HidDateFrom { get; set; }
        public string HidTypeRequest { get; set; }
        public string rdbIndeterminate_IsChecked { get; set; }
        public string rdbDetermined_IsChecked { get; set; }
        public string HidStatePrograming { get; set; }
        public string HidStateContract { get; set; }
        public string HidState { get; set; }
        public string HidBloqDes { get; set; }
        public string HidRoutePdf { get; set; }
        public string HidContract { get; set; }

        //datos del servicio del cliente
        public string Plan { get; set; }

        //Datos del usuario en session
        public string FullName { get; set; }


        //DATOS DE CLIENTE QUE SE MUESTRAN EN LA VISTA
        public string lblPhoneNumber { get; set; } //lblNroTelefono EN SIACPO
        public string lblCustomerType { get; set; }//lblTipoCliente EN SIACPO
        public string lblCustomerName { get; set; }//lblCliente EN SIACPO
        public string lblPlanName { get; set; }//lblPlan EN SIACPO
        public string lblCycleFact { get; set; }//lblCicloFacturación EN SIACPO
        public string txtEmail { get; set; }//txtEmail EN SIACPO
        public string LegalAgent { get; set; }//Representnate legal

        //programar activ/desactiv roamming
        public string txtDateDeact { get; set; }// textbox de fecha desaactivacion 
        public string txtDateAct { get; set; }// textbox de fecha de activacion



        //DATOS DE CLIENTE USO DE CONTROLADOR
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DniRuc { get; set; }
        public string PhoneReference { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerId { get; set; }
        public string Account { get; set; }





        public string HidRecord { get; set; }
        public string HidCodPackage {get; set;}
        public string HidBloqAct { get; set; }
        public string HidCodExclusive { get; set; }
        public string HidValidateInitial { get; set; }
        public string HidAccess { get; set; }
        public string HidStatusDisabledApa { get; set; }
        public string HidNameObj { get; set; }
        public string HidStateActiveCC { get; set; }
        public string HidDiffCFixedTotWithCFixed { get; set; }




        public string strMaxQuota {get; set;}
        public string strMinQuota {get; set;}
        public string strMaxPeriod {get; set;}
        public string strMinPeriod {get; set;}
        public string strPeriod {get; set;}
        public string strModQuotaPer {get; set;}
        public string strEnvioLog { get; set; }
        public string strEstOk { get; set; }
        public string strEstCancel { get; set; }



        //ValidarProgramacionDesactivacion

        public string StrStatus { get; set; }
        public string StrCodId { get; set; }
        public string StrCodSer { get; set; }
        public string StrPhone { get; set; }




        //prop para la venta de ConstModCP
        public string StrHidCodInter { get; set; }
        //Datos que se muestran en ConstModCP
        public string StrlblNumberPhone { get; set; }
        public string StrlblDNIRUC { get; set; }
        public string StrlblTypeClient { get; set; }
        public string StrlblClient { get; set; }
        public string StrlblContactClient { get; set; }
        public string StrlblContact { get; set; }
        public string StrlblServiceBusiness { get; set; }
        public string StrlblNewNumPerVal { get; set; }
        public string StrlblQuotModify { get; set; }
        public string StrlblSendEmail { get; set; }
        public string StrlblEmail { get; set; }
        public string StrlblCacDac { get; set; }
        public string StrlblChargeFixed { get; set; }
        public string StrlblDateExec { get; set; }
        public string StrlblNumPerVal { get; set; }
        public string StrtaskNoteModCP { get; set; }
        public string StrlblLegend { get; set; }
        public string StrStrlblLegendVisible { get; set; }

        //Datos para mostrar DetailCommercial
        public List<ServiceBSCS> ListServiceBSCS { get; set; }
        public string StrlblServiceCommercial { get; set; }

        public string gConstResultadoErrorBSCS { get; set; }
    }
}