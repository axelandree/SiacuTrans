using System.Web.Mvc;
using System.Web.Optimization;
using Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Admin;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions
{
    public class TransactionsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Transactions";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            RegisterBundles();

            context.MapRoute(
             "Fixed",
             "Transactions/Fixed/{controller}/{action}/{id}",
             new { action = "Index", id = UrlParameter.Optional },
             new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Fixed" }
            );

            context.MapRoute(
                "HFC",
                "Transactions/HFC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.HFC" }
            );

            context.MapRoute(
                "LTE",
                "Transactions/LTE/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.LTE" }
            );

            context.MapRoute(
            "DTH",
            "Transactions/DTH/{controller}/{action}/{id}",
            new { action = "Index", id = UrlParameter.Optional },
            new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.DTH" }
             );

            context.MapRoute(
                "Postpaid",
                "Transactions/Postpaid/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Postpaid" }
            );

            context.MapRoute(
                "Prepaid",
                "Transactions/Prepaid/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.Prepaid" }
            );

            context.MapRoute(
                "AuthUser",
                "Transactions/AuthUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Claro.SIACU.Web.WebApplication.Transac.Service.Areas.Transactions.Controllers.AuthUser" }
            );

             context.MapRoute(
               "Transactions",
              "Transactions/{controller}/{action}/{id}",
              new { action = "Index", id = UrlParameter.Optional }
              );
        }

        private void RegisterBundles()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }   
    }
}