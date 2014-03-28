using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class TimetablePeriod
	{
		public string Description { get; set; }
		public IEnumerable<Day> Day { get; set; }
	}
}
