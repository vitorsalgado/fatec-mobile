using System;
using System.Security.Principal;

namespace Fatec.Core.Domain
{
	public class FatecIdentity : IIdentity
	{
		private bool _isAuthenticated;
		private string[] _roles;
		public string Name { get; private set; }
		public string Fullname { get; private set; }
		public string Email { get; private set; }

		public FatecIdentity(string login, string fullName, string email, string[] roles)
		{
			if (string.IsNullOrEmpty(login)) throw new ArgumentNullException("login");
			if (string.IsNullOrEmpty(fullName)) throw new ArgumentNullException("fullName");
			
			Name = login;
			Fullname = fullName;
			Email = email;

			_roles = roles;
			_isAuthenticated = true;
		}

		public string AuthenticationType
		{
			get { return "custom"; }
		}

		public bool IsAuthenticated
		{
			get { return _isAuthenticated; }
		}

		public string[] Roles { get { return _roles; } }
	}
}
