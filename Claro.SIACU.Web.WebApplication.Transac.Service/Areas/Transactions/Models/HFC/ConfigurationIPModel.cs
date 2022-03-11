using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class ConfigurationIPModel
    {
        public string strFullName { get; set; }
        public string strFormatConstancy { get; set; }
        public string strBIRTHDAY { get; set; }
             
        public string strCodinssrv { get; set; }
        public string strCodSolot { get; set; }
        public string strSiteCode { get; set; }
        public string IdSession { get; set; }
        public string Id { get; set; }
        public string CodeTipification { get; set; }
        public string strCustomerId { get; set; }
        public string strTipoTransaccion { get; set; }
        public string strFlagContingencia { get; set; }
        public string strMsisdn { get; set; }
        public string strAccount { get; set; }
        public string strContactCode { get; set; }
        public int strFLAG_REGISTRADO { get; set; }
        #region Customer
        public string strTelephone {get;set;}
        public string strUser { get; set; }
        public string strName { get; set; }
        public string strLastName { get; set; }
        public string strFirtName { get; set; }
        public string strNameComplete { get; set; }
        public string strBusinessName { get; set; }
        public string strDocumentType { get; set; }
        public string strDocumentNumber { get; set; }
        public string strAddress { get; set; }
        public string strDistrict { get; set; }
        public string strDepartament { get; set; }
        public string strProvince { get; set; }
        public string strModality { get; set; }

        public string strLegalDepartament { get; set; }
        public string strLegalProvince { get; set; }
        public string strLegalDistrict { get; set; }
       
        public string strNroSot { get; set; }
        public string strLegalBuilding { get; set; }
        public string strLegalRepresentation { get; set; }
       

        public string strContractId { get; set; }
        public string strAddressInst { get; set; }
        public string strUbigeoInst { get; set; }
        public string strCodePlanInst { get; set; }
        public string strPlan { get; set; }
        public string strMonthEmision { get; set; }
        public string strYearEmision { get; set; }
        #endregion

        public string strTransaction { get; set; }
        public string strJobTypes { get; set; }
        public string strDescJobType { get; set; }
        public string strMotiveSot { get; set; }
        public string strDescMotiveSot { get; set; }
        public string strServices { get; set; }
        public string strDescServices { get; set; }
        public string strCacDac { get; set; }
        public string strDescCacDac { get; set; }
        public string strBranchCustomer { get; set; }
        public string strDescBranchCustomer { get; set; }
        public string strNote { get; set; }
        public bool bSendMail { get; set; }
        public string strEmail { get; set; }

        public string strSn { get; set; }
        public string strIpServidor { get; set; }
        
        public string strMensajeTransaccionFTTH { get; set; } //RONALDRR.
        public string strPlanoFTTH { get; set; } //RONALDRR.
        
    }
}