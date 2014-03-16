using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IAuthenticationService
	{
		FatecIdentity GetAuthenticatedUser();
		void SignIn(FatecIdentity user, bool persistent);
		void SignOut();
	}
}
