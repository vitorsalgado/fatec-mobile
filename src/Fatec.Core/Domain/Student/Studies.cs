namespace Fatec.Core.Domain
{
	public class StudiesAdvance : AbstractAuditedEntity
	{
		public string Registry { get; set; }
		public string Semester { get; set; }
		public string Period { get; set; }
		public string Situation { get; set; }
		public string TeacherDecision { get; set; }

		public int DisciplineId { get; set; }

		public Discipline Discipline { get; set; }
	}
}
