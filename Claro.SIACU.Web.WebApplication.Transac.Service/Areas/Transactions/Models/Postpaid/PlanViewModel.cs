using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class ListPlan
    {
        public string strCodeProduct { get; set; }
        public string strTMCode { get; set; }
        public string strDescriptPlan { get; set; }
        public string strVersion { get; set; }
        public string strCategProduct { get; set; }
        public string strCodeCartInf { get; set; }
        public string strDateIniVige { get; set; }
        public string strDateFinVige { get; set; }
        public string strIdTypeProduct { get; set; }
        public string strUser { get; set; }
    }
    public class PlanViewModel
    {
        public List<ListPlan> ListPlan { get; set; }
    }
}