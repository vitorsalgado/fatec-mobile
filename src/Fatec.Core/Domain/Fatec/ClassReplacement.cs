using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class ClassReplacement : AuditedEntity
	{
		public DateTime Date { get; set; }
		public IEnumerable<string> Periods { get; set; }
		public string TeacherName { get; set; }

		public int DisciplineId { get; set; }

		public Discipline _discipline;
		public Discipline Discipline
		{
			get { return _discipline ?? (_discipline = new Discipline(DisciplineId)); }
			set { _discipline = value; }
		}
	}
}
