using System;

namespace Fatec.Core.Domain
{
	public abstract class AuditedEntity : BaseEntity
	{
		public DateTime? CreatedOn { get; set; }
		public int CreatorId { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}
