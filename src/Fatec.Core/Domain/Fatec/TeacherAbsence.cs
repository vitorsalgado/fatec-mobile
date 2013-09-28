using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class TeacherAbsence : AuditedEntity
	{
		public string TeacherName { get; set; }
		public string Reason { get; set; }
		public IEnumerable<string> Periods { get; set; }
		public DateTime Date { get; set; }
		public string Semester { get; set; }
		public string Observations { get; set; }

		public int DisciplineId { get; set; }

		private Discipline _discipline;
		public Discipline Discipline
		{
			get { return _discipline ?? (_discipline = new Discipline(DisciplineId)); }
			set { _discipline = value; }
		}
	}
}
