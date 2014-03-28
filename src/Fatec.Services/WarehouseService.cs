using Fatec.Core.ApplicationServices;
using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class WarehouseService : IWarehouseService
	{
		private readonly IWarehouseRepository _wareHouseRepository;

		public WarehouseService(IWarehouseRepository wareHouseRepository)
		{
			if (wareHouseRepository == null) throw new ArgumentNullException("wareHouseRepository");
			_wareHouseRepository = wareHouseRepository;
		}

		public ICollection<KeyMovement> GetKeyMovement(KeyMovementCriteria criteria)
		{
			if (criteria == null) throw new ArgumentNullException("criteria");

			return _wareHouseRepository.GetKeyMovement(criteria);
		}
	}
}
