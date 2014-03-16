using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class ClassReplacement : AbstractAuditedEntity
	{
		public DateTime Date { get; set; }
		public IEnumerable<string> Periods { get; set; }
		public string TeacherName { get; set; }
		public int DisciplineId { get; set; }
		public virtual Discipline Discipline { get; set; }
	}
}
