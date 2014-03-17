using Fatec.Core.Domain;
using Fatec.Core.Services;
using System;
using System.Web;

namespace Fatec.Core.Infrastructure
{
	public class FatecWorkContext : IWorkContext
	{
		private readonly IAuthenticationService _authenticationService;
		private HttpContextBase _httpContext;
		private FatecIdentity _contextUser;

		public FatecWorkContext(HttpContextBase httpContext, IAuthenticationService authenticationService)
		{
			if (httpContext == null) throw new ArgumentNullException("httpContext");
			if (authenticationService == null) throw new ArgumentNullException("authenticationService");

			_httpContext = httpContext;
			_authenticationService = authenticationService;
		}

		public string CurrentUsername
		{
			get { return _httpContext.User.Identity.Name; }
		}

		public FatecIdentity CurrentUser
		{
			get { return (_contextUser ?? (_contextUser = _authenticationService.GetAuthenticatedUser())); }
		}
	}
}