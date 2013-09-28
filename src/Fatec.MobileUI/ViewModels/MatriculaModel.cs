namespace Fatec.MobileUI.ViewModels
{
	public class MatriculaModel
	{
		public string Disciplina { get; set; }
		public decimal NotaPrimeiroBimestre { get; set; }
		public int FaltasPrimeiroBimestre { get; set; }
		public decimal NotaSegundoBimestre { get; set; }
		public int FaltasSegundoBimestre { get; set; }
		public decimal Media { get; set; }
		public string Conceito { get; set; }
	}
}