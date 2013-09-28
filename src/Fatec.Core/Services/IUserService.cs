using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IUserService
	{
		SysUser GetByUsername(string username);
		bool ValidateUser(string username, string password);
	}
}
