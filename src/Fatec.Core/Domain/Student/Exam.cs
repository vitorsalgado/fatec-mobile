using System;

namespace Fatec.Core.Domain
{
	public class Exam : AuditedEntity
	{
		public string Professor { get; set; }
		public DateTime FirstExamDate { get; set; }
		public DateTime SecondExamDate { get; set; }
		public string Period { get; set; }

		public int DisciplineId { get; set; }

		private Discipline _discipline;
		public Discipline Discipline 
		{
			get { return _discipline ?? (_discipline = new Discipline(DisciplineId)); }
			set { _discipline = value; }
		}
	}
}
