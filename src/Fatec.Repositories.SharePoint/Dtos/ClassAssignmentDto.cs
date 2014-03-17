namespace Fatec.Repositories.Dtos
{
	public class ClassAssignmentDto
	{
		public string Schedule { get; set; }
		public string TeacherName { get; set; }
		public string Period { get; set; }
		public int DisciplineId { get; set; }
		public string ClassRoom { get; set; }
		public string Semester { get; set; }

		public string DisciplineName { get; set; }
	}
}
