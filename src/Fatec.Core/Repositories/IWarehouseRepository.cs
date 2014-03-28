using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IWarehouseRepository
	{
		ICollection<KeyMovement> GetKeyMovement(KeyMovementCriteria criteria);
	}
}
