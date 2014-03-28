using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.Core.Domain
{
	public class Day
	{
		public DayOfWeek DayOfWeek { get; set; }
		public IEnumerable<Schedule> Schedules { get; set; }

		public string Description
		{
			get { return DayOfWeek.ToString(); }
		}
	}
}
