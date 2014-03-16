using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Fatec.MobileUI.Infrastructure.Filters
{
	public class FatecAuthenticationFilterAttribute : FilterAttribute, IAuthorizationFilter
	{
		private const char SPLIT_CHAR = ';';

		private string[] _rolesArray;
		private string _roles = string.Empty;
		private string[] _usersArray;
		private string _users = string.Empty;

		public string Roles 
		{
			get
			{
				return _roles;
			}
			set
			{
				var arr = value.Split(SPLIT_CHAR);
				_rolesArray = new string[arr.Length];
				arr.CopyTo(_rolesArray, 0);
				_roles = value;
			}
		}

		public string Users
		{
			get
			{
				return _users;
			}
			set
			{
				var arr = value.Split(SPLIT_CHAR);
				_usersArray = new string[arr.Length];
				arr.CopyTo(_usersArray, 0);
				_users = value;
			}
		}

		public void OnAuthorization(AuthorizationContext filterContext)
		{
			if (filterContext == null)
				throw new ArgumentNullException("filterContext");

			if (string.IsNullOrEmpty(Roles) && string.IsNullOrEmpty(Users))
				return;
		}
	}
}
