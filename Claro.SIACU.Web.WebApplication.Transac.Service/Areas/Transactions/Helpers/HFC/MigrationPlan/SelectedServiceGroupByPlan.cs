using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Claro.SIACU.Web.WebApplication.Transac.Service.FixedTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan
{
    public class SelectedServiceGroupByPlan
    {
        public string strIdSession { get; set; }
        public List<ServiceByPlan> ServicesByPlan { get; set; }
    }
}
