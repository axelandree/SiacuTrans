using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan
{
    public class SelectedServiceConsolidate
    {
        public string strIdSession { get; set; }
        public string idPlan { get; set; }
        public List<SelectedService> services { get; set; }
    }
}