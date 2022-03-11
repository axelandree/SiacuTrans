using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.PlanMigration
{
    public class PostPaidPlanMigration
    {
        public PostPaidPlanMigration()
        { }

        public List<Helpers.CommonServices.GenericItem> listItemOpcTope { get; set; }
        public string hidOpTopeConsOrd { get; set; }
        public string hidOpTopeConsCod { get; set; }
        public string hidOpTopeConsDesc { get; set; }
        public string hidOpTopeCons5Soles { get; set; }
        public string hidValidaTipoCliente { get; set; }
        public string hidValidaCargoFijoSAP { get; set; }
        public string hidOpTopeCodAutomatico { get; set; }
        public string hidAuditFechProg { get; set; }
        public string hidAuditChckFideliza { get; set; }
        public string hidAuditChckSinCosto { get; set; }
        public string hidPorcentajeIgv { get; set; }
        public string hidAccesoCheck { get; set; }
        public string hidAccesoFechProg { get; set; }
        public string hidDiferenciaMontoCF { get; set; }
        public string hidAccesoTopeSinCosto { get; set; }
        public string hidCodSerConsumoAdi { get; set; }
        public string hidCobroApadece { get; set; }
        public string hidConsumerControl { get; set; }
        public string hidCorporativo { get; set; }
        public string hidFechaActual { get; set; }
        public string hidFechaLimite { get; set; }
        public string TipoClienteAplica { get; set; }
        public string txtCobroApadece { get; set; }
        public string chkFideliza { get; set; }
        public string chkOcc { get; set; }
        public string txtMontoSIGA { get; set; }
        public string txtCobroApadeceEnable { get; set; }
        public string txtMontoFidelizaApadece { get; set; }
        public string gConstMsjPlanClaroConexionChip { get; set; }
        public string gCodCostoCero { get; set; }
        public string CodPlanClaroConexionChip { get; set; }
        public string CodPlanSinTopeConsAutorizacion { get; set; }
        public string txtTotalApadeceCobrar { get; set; }
        public string txtCobroPenalidadPCS { get; set; }
        public string txtMontoPenalidadPCS { get; set; }
        public string txtTotalPenalidadPCS { get; set; }
        public string strConfigPlantaforma { get; set; }

    }
}