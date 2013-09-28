using System;
using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class TeacherAbsenceModel
	{
		public string Professor { get; set; }
		public string Motivo { get; set; }
		public string Disciplina { get; set; }
		public IEnumerable<string> Turnos { get; set; }
		public DateTime Data { get; set; }
		public string Semestre { get; set; }
		public string Observacoes { get; set; }
	}
}