namespace Fatec.Core.Domain
{
	public class EnrolledDiscipline : AbstractAuditedEntity
	{
		public string Registry { get; set; }
		public string Horary { get; set; }
		public string Period { get; set; }
		public string Situation { get; set; }
		public string History { get; set; }
		public int AbsencesFirstTwoMonths { get; set; }
		public decimal GradeP1 { get; set; }
		public int AbsencesSecondTwoMonths { get; set; }
		public decimal GradeP2 { get; set; }
		public decimal WorkGrade { get; set; }
		public decimal AbsencesPercent { get; set; }
		public decimal PartialGrade { get; set; }
		public decimal Grade { get; set; }
		public string Concept { get; set; }
		public int DisciplineId { get; set; }
		public Discipline Discipline { get; set; }
	}
}
