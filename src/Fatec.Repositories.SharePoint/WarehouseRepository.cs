using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.SharePoint.Mapping;
using System;
using System.Collections.Generic;

namespace Fatec.Repositories.SharePoint
{
	public class WarehouseRepository : IWarehouseRepository
	{
		private const string _path = "/tic";
		private ISPDbContext _context;

		public WarehouseRepository(ISPDbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		public ICollection<KeyMovement> GetKeyMovement(KeyMovementCriteria criteria)
		{
			if (criteria == null) throw new ArgumentNullException("criteria");

			var viewFields = _context.CreateViewFields(
				"ID", "Requisitante", "Data_x0020_de_x0020_Retirada", "Chave");

			string query = string.Empty;

			if (string.IsNullOrEmpty(criteria.Requester))
			{
				query =
				@"<Query>
					<Where>
						<IsNull><FieldRef Name='Data_x0020_de_x0020_Devolu_x00e7'/></IsNull>
					</Where>
					<OrderBy><FieldRef Name='Requisitante' Ascending='True'/></OrderBy>
				</Query>";
			}
			else
			{
				query = string.Format(@"<Query>
					<Where>
						<And>
							<IsNull><FieldRef Name='Data_x0020_de_x0020_Devolu_x00e7'/></IsNull>
							<Contains><FieldRef Name='Requisitante' /><Value Type='Text'>{0}</Value></Contains>
						<And>
					</Where>
					<OrderBy><FieldRef Name='Requisitante' Ascending='True'/></OrderBy>
				</Query>", criteria.Requester);
			}

			return _context.ExecuteQuery<KeyMovement>(
				_path, "Controle de Chaves", query, viewFields, WarehouseMap.Map);
		}
	}
}
