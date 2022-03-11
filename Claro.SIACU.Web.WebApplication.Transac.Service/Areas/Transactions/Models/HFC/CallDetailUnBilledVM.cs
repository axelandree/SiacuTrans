using System.Collections.Generic;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.HFC.CallDetails;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models.HFC
{
    public class CallDetailUnBilledVM
    {
        public string VTotal { get; set; }
        public string StrResponseCode { get; set; }
        public string StrResponseMessage { get; set; }
        public List<UnBilledCallsDetail> LstPhoneCall { get; set; }
    }
}