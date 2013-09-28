using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class Timetable
	{
		public int CourseId { get; set; }
		public Course Course { get; set; }
		public IEnumerable<TimetablePeriod> Periods { get; set; }
	}
}
