using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;

namespace Fatec.Core.Domain
{
	public class Discipline : AbstractAuditedEntity
	{
		public string Acronym { get; set; }
		public string Name { get; set; }
		public string Cycle { get; set; }
		public decimal Workload { get; set; }
		public decimal TotalWorkload { get; set; }
		public decimal Credits { get; set; }

	}
}
