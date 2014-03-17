using Fatec.Core.Domain;
using Fatec.Core.Services;
using Fatec.MobileUI.Filters;
using Fatec.MobileUI.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
    public class AccountController : Controller
    {
		private readonly IAuthenticationService _authenticationService;
		private readonly IUserService _userService;

		public AccountController(IAuthenticationService authenticationService, IUserService userService)
		{
			_authenticationService = authenticationService;
			_userService = userService;
		}

		[AllowAnonymous]
		[BackButtonAction("Index", "Home")]
        public ActionResult Login(string returnUrl)
        {
			ViewBag.ReturnUrl = returnUrl;
            return View();
        }

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[BackButtonAction("Index", "Home")]
		public async Task<ActionResult> Login(LoginModel model, string returnUrl)
		{
			//if (ModelState.IsValid)// && _userService.ValidateUser(model.Username, model.Password))
			//{
				//var user = _userService.GetUserByUsername(model.Username);
				var user = new FatecIdentity(
					"1290371313006",
					"Vitor Hugo Salgado",
					"vsalgadopb@gmail.com",
					(new string[]{ "Aluno FATEC" }));

				await Task.Run(() => _authenticationService.SignIn(user, model.Remember));

				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);
				else
					return RedirectToAction("Index", "Home", null);
			//}

			ModelState.AddModelError("", "O usuário ou senha informados estão incorretos.");

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Logout()
		{
			_authenticationService.SignOut();
			return RedirectToAction("Index", "Home");
		}
    }
}
