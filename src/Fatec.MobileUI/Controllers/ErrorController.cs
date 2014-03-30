using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
	[AllowAnonymous]
	public class ErrorController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult NotFound()
		{
			return View("404");
		}
	}
}
