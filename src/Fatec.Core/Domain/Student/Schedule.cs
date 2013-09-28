namespace Fatec.Core.Domain
{
	public class Schedule
	{
		public string TeacherName { get; set; }
		public string ClassRoom { get; set; }

		public int DisciplineId { get; set; }

		private Discipline _discipline;
		public Discipline Discipline
		{
			get { return _discipline ?? (_discipline = new Discipline(DisciplineId)); }
			set { _discipline = value; }
		}
	}
}
