using Fatec.Core.Domain;

namespace Fatec.Core.Services
{
	public interface IAuthenticationService
	{
		FatecIdentity GetAuthenticatedUser();
		void SignIn(FatecIdentity user, bool persistent);
		void SignOut();
	}
}
