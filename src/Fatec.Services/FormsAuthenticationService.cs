using Fatec.Core.Domain;
using Fatec.Core.Services;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Fatec.Services
{
	public class FormsAuthenticationService : IAuthenticationService
	{
		private readonly HttpContextBase _httpContext;
		private FatecIdentity _cachedUser = null;

		public FormsAuthenticationService(HttpContextBase httpContext)
		{
			_httpContext = httpContext;
		}

		public FatecIdentity GetAuthenticatedUser()
		{
			if (_cachedUser != null)
				return _cachedUser;

			if(!(_httpContext.User.Identity is FormsIdentity))
				return null;

			var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
			string[] userdata = formsIdentity.Ticket.UserData.Split(';');

			string login = formsIdentity.Ticket.Name;
			string fullname = userdata[0];
			string email = userdata[1];
			string[] roles = userdata[2].Split('|');

			var user = new FatecIdentity(login, fullname, email, roles);

			_cachedUser = user;
			
			return _cachedUser;
		}

		public void SignIn(FatecIdentity user, bool createPersistentCookie)
		{
			if (user == null) throw new ArgumentNullException("user");

			var now = DateTime.UtcNow.ToLocalTime();
			var roles = string.Join("|", user.Roles.ToArray());
			string userData = string.Format("{0};{1};{2}", user.Fullname, user.Email, roles);

			var ticket = new FormsAuthenticationTicket(
				1,
				user.Login,
				now,
				now.Add(FormsAuthentication.Timeout),
				createPersistentCookie,
				userData,
				FormsAuthentication.FormsCookiePath);

			var encryptedTicket = FormsAuthentication.Encrypt(ticket);
			var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

			cookie.HttpOnly = true;

			if (ticket.IsPersistent)
				cookie.Expires = ticket.Expiration;

			cookie.Secure = FormsAuthentication.RequireSSL;
			cookie.Path = FormsAuthentication.FormsCookiePath;

			if (FormsAuthentication.CookieDomain != null)
				cookie.Domain = FormsAuthentication.CookieDomain;

			_httpContext.Response.Cookies.Add(cookie);
		}

		public void SignOut()
		{
			_cachedUser = null;
			FormsAuthentication.SignOut();
		}
	}
}
