using Fatec.Core.Domain;

namespace Fatec.Core
{
	public interface IWorkContext
	{
		string CurrentUsername { get; }
		FatecIdentity CurrentUser { get; }
		FatecPrincipal CurrentPrincipal { get; }
	}
}
