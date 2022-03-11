using System.Collections.Generic;
using Claro.SIACU.Entity.Transac.Service.Fixed;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan;
using AuditRegister = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.AuditRegister;
using SotPending = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.SotPending;
using GenerateConstancy = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.GenerateConstancy;
using ImplementLoyalty = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.ImplementLoyalty;
using ImplementOcc = Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService.ImplementOcc;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.LTE
{
    public class InstallUninstallDecoderModel
    {
        public string StrIdSession { get; set; }
        public string StrContractId { get; set; }
        public string StrCustomerId { get; set; }
        public ParametersSaved Type { get; set; }
        public InteractionBpelModel InteractionModel { get; set; }
        public InteractionPlusBpelModel InsInteractionPlusModel { get; set; }
        public List<DecoModel> Decos { get; set; }
        public string StrAmountIgv { get; set; }
        public InteractionModel Typification { get; set; }
        public DecoCustomerModel DecoCustomerModel { get; set; }
        public AuditRegister AuditRegister { get; set; }
        public SotPending SotPending { get; set; }
        public PostSaleProcessModel RegistrarProcesoPostventa { get; set; }
        public string LoyaltyAmount { get; set; }
        public int LoyaltyFlag { get; set; }
        public GenerateConstancy GenerateConstancy { get; set; }
        public ImplementLoyalty ImplementLoyalty { get; set; }
        public ImplementOcc ImplementOcc { get; set; }
        public string EtaValidation { get; set; }
        public RegistrarEtaSeleccion RegistrarEtaSeleccion { get; set; }
        public RegistrarEta RegistrarEta { get; set; }
    }
}