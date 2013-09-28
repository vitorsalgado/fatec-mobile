using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IAuthenticationService
	{
		SysUser GetAuthenticatedUser();
		void SignIn(SysUser user, bool createPersistentCookie);
		void SignOut();
	}
}
