using Fatec.Core.Domain;

namespace Fatec.Core
{
	public interface IWorkContext
	{
		string CurrentUsername { get; }
		SysUser CurrentUser { get; }
	}
}
