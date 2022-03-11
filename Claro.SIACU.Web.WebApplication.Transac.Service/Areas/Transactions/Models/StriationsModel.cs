using System.Collections.Generic;
using HelperItem = Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Helpers.Postpaid;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Models
{
    public class StriationsModel
    {
        public List<HelperItem.StriationHelper> Striations { get; set; }
    }
}