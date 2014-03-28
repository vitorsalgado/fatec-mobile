using System;
using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class TeacherAbsenceModel
	{
		public string Teacher { get; set; }
		public string Reason { get; set; }
		public string Discipline { get; set; }
		public IEnumerable<string> Periods { get; set; }
		public DateTime Date { get; set; }
		public string Semester { get; set; }
		public string Details { get; set; }
	}
}