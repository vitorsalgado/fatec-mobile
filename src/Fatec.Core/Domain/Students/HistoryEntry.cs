using System;

namespace Fatec.Core.Domain
{
	public class HistoryEntry
	{
		public String Period { get; set; }
		public String Semester { get; set; }
		public decimal Average { get; set; }
		public String Concept { get; set; }
		public Discipline Discipline { get; set; }
	}
}
