using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Prepaid
{
    public class TFIPlanMigrationModel
    {
        public string IdSession { get; set; }
        
        public string strPuntoAtencion { get; set; }
        public string strCodeTipification { get; set; }
        public string strNote { get; set; }
        public string strNewPlanDescription { get; set; }
        public string strPlanDescription { get; set; }
        public string strEmail { get; set; }
        public string strTelephone { get; set; }
        public string strCurrentUser { get; set; }
        public string strNameUser { get; set; }
        public string strFullName { get; set; }
        public string strLegalAgent { get; set; }
        public string strLogin { get; set; }
        public string strisTFI { get; set; }
        public string strDocument { get; set; }
        public string strDocumentType { get; set; }
        public string strDateTransaction { get; set; }
        public string strDateActivation { get; set; }
        public string strStateLine { get; set; }
        public string strAction { get; set; }
        public string strIdTipification { get; set; }
        public string strMessageValidateState { get; set; }
        public string strCustomerCode { get; set; }
        public string strSubClaseCode { get; set; }
        public string strSubClaseDescription { get; set; }
        public bool bGeneratedPDF { get; set; }
        public bool bSendMail { get; set; }
        public bool bErrorTransac { get; set; }
        public bool bErrorInteract { get; set; }
        public bool bValidateState { get; set; }
        public string strMessageErrorTransac { get; set; }
        public string strProvider { get; set; }
        public string strTariff { get; set; }
        public string strSuscriber { get; set; }
        public string strOfferDescription { get; set; }
        public string strNewOfferDescription { get; set; }
        public string strMessageError { get; set; }

        public string strObjidContacto { get; set; }
        public string strRoutePDF { get; set; }
    }
}