using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class History
	{
		public String Enrollment { get; set; }
		public List<HistoryEntry> Entries { get; set; }
		public decimal EfficiencyPercent { get; set; }

		public decimal TotalCredits
		{
			get
			{
				decimal total = 0;

				foreach (var entry in Entries)
					total += entry.Discipline.Credits;

				return total;
			}
		}

		public decimal TotalWorkload
		{
			get
			{
				decimal total = 0;

				foreach (var entry in Entries)
					total += entry.Discipline.TotalWorkload;

				return total;
			}
		}
	}
}
