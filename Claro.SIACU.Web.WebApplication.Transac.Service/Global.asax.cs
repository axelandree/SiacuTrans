using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using Claro.SIACU.Web.WebApplication.Transac.Service.App_Start;
using Claro.SIACU.Web.WebApplication.Transac.Service.Mappings;

namespace Claro.SIACU.Web.WebApplication.Transac.Service
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AutoMapperConfig.RegisterMappings();
        }
    }
}
