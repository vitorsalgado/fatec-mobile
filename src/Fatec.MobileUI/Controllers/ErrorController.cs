using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
    public class ErrorController : Controller
    {
		public ActionResult NotFound()
		{
			return View();
		}
    }
}
