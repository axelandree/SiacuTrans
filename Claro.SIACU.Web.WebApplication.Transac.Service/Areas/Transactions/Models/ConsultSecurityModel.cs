using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Claro.SIACU.Web.WebApplication.Transac.Service.CommonTransacService;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class ConsultSecurityModel
    {
        public string ErrMessage { get; set; }
        public string CodeErr { get; set; }
        public List<SecurityModel> ListConsultSecurity { get; set; }
    }
}