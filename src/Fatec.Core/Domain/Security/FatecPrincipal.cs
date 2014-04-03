using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Fatec.Core.Domain
{
	public class FatecPrincipal : IPrincipal
	{
		private IIdentity _identity;
		private string[] _roles;

		public FatecPrincipal(IIdentity identity, string[] roles)
		{
			if (identity == null) throw new ArgumentNullException("identity");
			if (roles == null) throw new ArgumentNullException("roles");

			_identity = identity;
			_roles = new string[roles.Length];
			roles.CopyTo(_roles, 0);
			Array.Sort(_roles);
		}

		public IIdentity Identity
		{
			get { return _identity; }
		}

		public bool IsInRole(string role)
		{
			return Array.BinarySearch(_roles, role) >= 0;
		}

		public bool IsInAllRoles(params string[] roles)
		{
			if (roles == null) throw new ArgumentNullException("roles");

			foreach (string searchrole in roles)
				if (Array.BinarySearch(_roles, searchrole) < 0)
					return false;

			return true;
		}

		public ICollection<string> Roles { get { return _roles; } }
	}
}
