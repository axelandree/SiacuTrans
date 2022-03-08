using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.MigrationPlan
{
    public class ValidationInfo
    {
        public ValidationInfo()
        {
            Codigo = string.Empty;
            Codigo2 = string.Empty;
            Descripcion = string.Empty;
        }
        public string Codigo { get; set; }
        public string Codigo2 { get; set; }
        public string Descripcion { get; set; }
    }
}
