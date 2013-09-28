namespace Fatec.Core.Domain
{
	public class StudiesAdvance : AuditedEntity
	{
		public string Registry { get; set; }
		public string Semester { get; set; }
		public string Period { get; set; }
		public string Situation { get; set; }
		public string TeacherDecision { get; set; }

		public int DisciplineId { get; set; }

		private Discipline _discipline;
		public Discipline Discipline 
		{
			get { return _discipline ?? (_discipline = new Discipline(DisciplineId)); }
			set { _discipline = value; }
		}
	}
}
