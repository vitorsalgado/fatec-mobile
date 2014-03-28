namespace Fatec.Core.Domain
{
	public class Schedule
	{
		public string TeacherName { get; set; }
		public string ClassRoom { get; set; }

		public int DisciplineId { get; set; }

		public Discipline Discipline { get; set; }
	}
}
