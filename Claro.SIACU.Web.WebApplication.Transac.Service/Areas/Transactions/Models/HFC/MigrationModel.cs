using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class MigrationModel
    {
        public string Rules { get; set; }

        public List<FixedTransacService.ProductPlanHfc> plans { get; set; }

        public IEnumerable<string> Campaigns { get; set; }

        public IEnumerable<string> Solutions { get; set; }

        public List<FixedTransacService.ServiceByPlan> ServicesByPlan { get; set; }

        public List<FixedTransacService.JobType> JobTypes { get; set; }

        public List<Helpers.CommonServices.Fixed.OrderSubTypeViewModel> OrderSubTypes { get; set; }

        public string ServerDate { get; set; }
        public List<FixedTransacService.Carrier> Carriers { get; set; }

        public string IdServicio { get; set; }


        public List<FixedTransacService.ServiceByCurrentPlan> ServicesByCurrentPlan { get; set; }
        public Helpers.HFC.MigrationPlan.PlanCharges ServicesByCurrentPlanCharges { get; set; }

        public List<FixedTransacService.ServiceByPlan> AditionalServices { get; set; }

        public string[] SelectedServices { get; set; }
        public string strServerPathPDF { get; set; }

        public List<FixedTransacService.ServiceByPlan> CoreServices { get; set; }

        public Helpers.HFC.MigrationPlan.ConfigurationData ConfigurationData { get; set; }
        public string strEstadoContratoInactivo { get; set; }
        public string strEstadoContratoSuspendido { get; set; }
        public string strEstadoContratoReservado { get; set; }
        public string strMsjEstadoContratoInactivo { get; set; }
        public string strMsjEstadoContratoSuspendido { get; set; }
        public string strMsjEstadoContratoReservado { get; set; }
        public string strMensajeTransaccionFTTH { get; set; }   //RONALDRR 
        public string strPlanoFTTH { get; set; }   //RONALDRR 
    }

    public class PlanMigrationLoadModel
    {
        public List<Helpers.LoadSelectHelper> lstWorkType { get; set; }
        public List<Helpers.LoadSelectHelper> lstSubWorkType { get; set; }
        public List<Helpers.LoadSelectHelper> lstCarriers { get; set; }
    }
}