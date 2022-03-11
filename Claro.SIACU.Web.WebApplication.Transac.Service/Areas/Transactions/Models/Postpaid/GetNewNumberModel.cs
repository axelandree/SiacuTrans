using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.Postpaid
{
    public class GetNewNumberModel
    {
        public string NEW_PHONE { get; set; }
        public string END_HLR { get; set; }
        public string MESSAGE { get; set; }
        public string ERROR { get; set; }
        public string ROLLBACK { get; set; }

    }
}