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
		private SysUser _cachedUser = null;

		public FormsAuthenticationService(HttpContextBase httpContext)
		{
			_httpContext = httpContext;
		}

		public SysUser GetAuthenticatedUser()
		{
			if (_cachedUser != null)
				return _cachedUser;

			if(!(_httpContext.User.Identity is FormsIdentity))
				return null;

			var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
			string[] userdata = formsIdentity.Ticket.UserData.Split(';');
			
			var user = new SysUser();
			user.Username = formsIdentity.Ticket.Name;
			user.Fullname = userdata[0];
			user.Email = userdata[1];
			foreach(var role in userdata[2].Split('|'))
				user.SysRoles.Add(new SysRole() { Name = role });

			_cachedUser = user;
			
			return _cachedUser;
		}

		public void SignIn(SysUser user, bool createPersistentCookie)
		{
			if (user == null) throw new ArgumentNullException("user");

			var now = DateTime.UtcNow.ToLocalTime();
			var roles = string.Join("|", user.SysRoles.Select(x => x.Name));
			string userData = string.Format("{0};{1};{2}", user.Fullname, user.Email, roles);
			user.Username = user.Username.ToUpper();

			var ticket = new FormsAuthenticationTicket(
				1,
				user.Username,
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
