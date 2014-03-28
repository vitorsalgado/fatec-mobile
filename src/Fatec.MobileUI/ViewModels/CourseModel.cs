using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class CourseModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TeachingLevel { get; set; }
		public string TeachingProfile { get; set; }
		public int DurationInMonths { get; set; }
		public int Workload { get; set; }
		public IEnumerable<string> Periods { get; set; }
	}
}