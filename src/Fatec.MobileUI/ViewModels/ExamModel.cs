using System;

namespace Fatec.MobileUI.ViewModels
{
	public class ExamModel
	{
		public string TeacherName { get; set; }
		public DateTime FirstExamDate { get; set; }
		public DateTime SecondExamDate { get; set; }
		public string Period { get; set; }
		public string DisciplineName { get; set; }
	}
}