using System;
using System.Collections;
namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class LineViewModel
    {
        public LineViewModel()
        {	
        }
        private string strNroPhone { get; set; }
        private string strStateLine { get; set; }
        private string strBalanceMain { get; set; }
        private string strDateExpirationBalance { get; set; }
        private string strChangeTriosFree { get; set; }
        private string strChangeTarifeFree { get; set; }
        private string strPlanTarife { get; set; }
        private string strDateActivation{ get; set; }
        private string strDateBol{ get; set; }
        private string strDateExpiration{ get; set; }
        private string strNroIMSI { get; set; }
        private string strStateIMSI{ get; set; }
        private string _NroICCID{ get; set; }
        private string strNroFamAmigos{ get; set; }
        private string strTipoTriacion{ get; set; }
        private string strProviderID{ get; set; }
        private DateTime dtDateState{ get; set; }
        private string strMotive{ get; set; }
        private string strPlan{ get; set; }
        private string strPlazeContract{ get; set; }
        private string strSeller{ get; set; }
        private string strCampain{ get; set; }
        private DateTime strValido_Desde{ get; set; }
        private string strCambiado_Por{ get; set; }
        private string strFlag_Plataforma{ get; set; }
        private string strPIN1{ get; set; }
        private string strPIN2{ get; set; }
        private string strPUK1{ get; set; }
        private string strPUK2{ get; set; }
        private string strContractID{ get; set; }
        private string strCodePlanTarife{ get; set; }
        private string strCNTNumber{ get; set; }
        private string strIsCNTPossible{ get; set; }
        private string strSubscriberState{ get; set; }
        private string strStateAcuerdo{ get; set; }
        private string strDateFindAcuerdo{ get; set; }
        private string strFlag_TFI{ get; set; }
        private string strEsTFI{ get; set; }
        private ArrayList strServicePlanCombo{ get; set; }
        private string strAccount{ get; set; }
        private string strDateDesactication{ get; set; }
        private string strTicket{ get; set; }

        public string _strCodePlanTarife
        {
            set { strCodePlanTarife = value; }
            get { return strCodePlanTarife; }
        }
    }
}