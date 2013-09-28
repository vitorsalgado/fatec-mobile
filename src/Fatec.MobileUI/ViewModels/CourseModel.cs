using System.Collections.Generic;

namespace Fatec.MobileUI.ViewModels
{
	public class CourseModel
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string NivelDeEnsino { get; set; }
		public string PerfilDeEnsino { get; set; }
		public int DuracaoEmMeses { get; set; }
		public int CargaHorario { get; set; }
		public IEnumerable<string> Turnos { get; set; }
	}
}