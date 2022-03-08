using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oTransacServ = Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan
{
    public static class CalculatePlanCharges
    {
        public static PlanCharges CalculateCharges(List<FixedTransacService.ServiceByCurrentPlan> services)
        {
            PlanCharges planCharges = new PlanCharges();
            var ValidGroups = new[]{"1","2","3","4","5","6","7"};
            planCharges.MontoActualBase = services.AsEnumerable().Where(o => ValidGroups.Contains(o.NoGrp) && o.ServiceType == oTransacServ.Constants.PresentationLayer.CORE).Sum(o => Convert.ToDecimal(o.CargoFijo));
            planCharges.CantidadServicios = services.AsEnumerable().Where(o => ValidGroups.Contains(o.NoGrp)).Count();
            planCharges.MontoActualAdicional = services.AsEnumerable().Where(o => ValidGroups.Contains(o.NoGrp) && o.ServiceType == oTransacServ.Constants.PresentationLayer.ADICIONAL).Sum(o => Convert.ToDecimal(o.CargoFijo));
            return planCharges;
        }

 
    }
}