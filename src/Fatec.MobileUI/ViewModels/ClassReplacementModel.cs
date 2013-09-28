using System;
using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class ClassReplacementModel
	{
		public DateTime Data { get; set; }
		public string Disciplina { get; set; }
		public IEnumerable<string> Turnos { get; set; }
		public string Professor { get; set; }
	}
}