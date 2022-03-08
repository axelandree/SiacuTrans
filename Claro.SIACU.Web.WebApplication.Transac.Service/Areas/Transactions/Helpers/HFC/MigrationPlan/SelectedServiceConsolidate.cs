using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan
{
    public class SelectedServiceConsolidate
    {
        public string strIdSession { get; set; }
        public string idPlan { get; set; }
        public string strServiceCoreInternet { get; set; }
        public string strServiceCoreCable { get; set; }
        public string strServiceCoreTelephony { get; set; }
    
        public List<SelectedService> services { get; set; }
    }
}