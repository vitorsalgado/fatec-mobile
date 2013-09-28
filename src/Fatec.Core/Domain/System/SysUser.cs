using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class SysUser
	{
		public string Username { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }

		private ICollection<SysRole> _sysRoles;
		public ICollection<SysRole> SysRoles
		{
			get { return _sysRoles ?? (_sysRoles = new List<SysRole>()); }
		}
	}
}
