using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.ApplicationServices
{
	public interface IWarehouseService
	{
		ICollection<KeyMovement> GetKeyMovement(KeyMovementCriteria criteria);
	}
}
