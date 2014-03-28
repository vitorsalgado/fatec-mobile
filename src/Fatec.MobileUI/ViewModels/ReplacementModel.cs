using System;
using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class ReplacementModel
	{
		public DateTime Date { get; set; }
		public string Discipline { get; set; }
		public IEnumerable<string> Periods { get; set; }
		public string Teacher { get; set; }
	}
}