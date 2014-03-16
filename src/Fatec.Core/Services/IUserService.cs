using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IUserService
	{
		FatecIdentity GetByUsername(string username);
		bool ValidateUser(string username, string password);
	}
}
