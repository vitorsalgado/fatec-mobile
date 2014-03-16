using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class Course : AbstractAuditedEntity
	{
		public string Name { get; set; }
		public string EducationLevel { get; set; }
		public string TeachingProfile { get; set; }
		public int DurationInMonths { get; set; }
		public int WorkLoad { get; set; }
		public IEnumerable<string> Periods { get; set; }
	}
}
