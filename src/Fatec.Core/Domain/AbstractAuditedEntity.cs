using System;

namespace Fatec.Core.Domain
{
	public abstract class AbstractAuditedEntity : AbstractEntity
	{
		public DateTime? CreatedOn { get; set; }
		public int CreatorId { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
