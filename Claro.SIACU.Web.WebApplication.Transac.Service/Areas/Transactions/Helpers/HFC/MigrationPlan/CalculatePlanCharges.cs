using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using oTransacServ = Claro.SIACU.Transac.Service;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan
{
    public static class CalculatePlanCharges
    {
        public static PlanCharges CalculateCharges(List<FixedTransacService.ServiceByCurrentPlan> services)
        {
            PlanCharges planCharges = new PlanCharges();
            var ValidGroups = new[]{"1","2","3","4","5","6"};
            planCharges.MontoActualBase = string.Format("{0:0.00}", services.AsEnumerable().Where(o => ValidGroups.Contains(o.NoGrp) && o.ServiceType == oTransacServ.Constants.PresentationLayer.CORE).Sum(o => Convert.ToDouble(o.CargoFijo)));
            planCharges.CantidadServicios = services.AsEnumerable().Where(o=>ValidGroups.Contains(o.NoGrp)).Count();
            planCharges.MontoActualAdicional = string.Format("{0:0.00}", services.AsEnumerable().Where(o => ValidGroups.Contains(o.NoGrp) && o.ServiceType == oTransacServ.Constants.PresentationLayer.ADICIONAL).Sum(o => Convert.ToDouble(o.CargoFijo)));
            return planCharges;
        }

 
    }
}