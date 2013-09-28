using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
    public class LogController : Controller
    {
		//[Authorize(Roles="Admins. do Domínio")]
        public ActionResult Index()
        {
			return View();
        }
    }
}
