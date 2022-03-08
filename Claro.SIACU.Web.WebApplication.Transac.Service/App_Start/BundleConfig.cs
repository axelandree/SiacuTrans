using System.Web.Optimization;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.App_Start
{
    static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;




        }
    }
}