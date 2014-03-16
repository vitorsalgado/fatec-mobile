using System;

namespace Fatec.Core.Domain
{
	public class Requirement : AbstractAuditedEntity
	{
		public string Description { get; set; }
		public string Category { get; set; }
		public string Comments { get; set; }
		public DateTime EndDate { get; set; }
		public string Result { get; set; }
	}
}
