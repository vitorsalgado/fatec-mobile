using Fatec.Core;
using Fatec.Core.Domain;
using Fatec.Core.Services;
using System.Web;

namespace Fatec.Core.Infrastructure
{
	public class FatecWorkContext : IWorkContext
	{
		private readonly IAuthenticationService _authenticationService;
		private HttpContextBase _httpContext;
		private SysUser _contextUser;

		public FatecWorkContext(HttpContextBase httpContext, IAuthenticationService authenticationService)
		{
			_httpContext = httpContext;
			_authenticationService = authenticationService;
		}

		public string CurrentUsername
		{
			get { return _httpContext.User.Identity.Name; }
		}

		public SysUser CurrentUser
		{
			get { return (_contextUser ?? (_contextUser = _authenticationService.GetAuthenticatedUser())); }
		}
	}
}