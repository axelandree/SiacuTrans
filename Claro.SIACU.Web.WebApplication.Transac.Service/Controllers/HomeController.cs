using System.Web.Mvc;

namespace Claro.SIACU.Web.WebApplication.Transac.Service.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DialogTemplate()
        {
            return View();
        }
	}
}