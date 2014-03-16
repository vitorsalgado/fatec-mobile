using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class FatecIdentity
	{
		private string[] _roles;
		public string Login { get; private set; }
		public string Fullname { get; private set; }
		public string Email { get; private set; }

		public FatecIdentity(string login, string fullName, string email, string[] roles)
		{
			if (string.IsNullOrEmpty(login)) throw new ArgumentNullException("login");
			if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException("fullName");
			
			Login = login;
			Fullname = fullName;
			Email = email;
			_roles = new string[roles.Length];
			roles.CopyTo(_roles, 0);
			Array.Sort(_roles);
		}

		public bool IsInRole(string role)
		{
			return Array.BinarySearch(_roles, role) >= 0;
		}

		public bool IsInAllRoles(params string[] roles)
		{
			foreach (string searchrole in roles)
				if (Array.BinarySearch(_roles, searchrole) < 0)
					return false;

			return true;
		}

		public ICollection<string> Roles { get { return _roles; } }
	}
}
