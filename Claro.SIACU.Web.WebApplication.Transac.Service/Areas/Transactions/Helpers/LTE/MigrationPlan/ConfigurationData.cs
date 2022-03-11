using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.LTE.MigrationPlan
{
    public class ConfigurationData
    {
        public string strLTEGroupCable { get; set; }

        public string strLTEGroupInternet { get; set; }

        public string strLTEGroupTelephony { get; set; }

        public string[] cables { get; set; }

        public string[] internets { get; set; }

        public string[] telephonys { get; set; }

        public string strPlanActualCable { get; set; }

        public string strPlanActualInternet { get; set; }

        public string strPlanActualTelephony { get; set; }

        public int intOneCoreCable { get; set; }

        public int intOneCoreInternet { get; set; }

        public int intOneCorePhone { get; set; }
    }
}
