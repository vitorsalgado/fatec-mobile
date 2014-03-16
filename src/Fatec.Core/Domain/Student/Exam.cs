using System;

namespace Fatec.Core.Domain
{
	public class Exam : AbstractAuditedEntity
	{
		public string Professor { get; set; }
		public DateTime FirstExamDate { get; set; }
		public DateTime SecondExamDate { get; set; }
		public string Period { get; set; }

		public int DisciplineId { get; set; }

		public Discipline Discipline { get; set; }
	}
}
