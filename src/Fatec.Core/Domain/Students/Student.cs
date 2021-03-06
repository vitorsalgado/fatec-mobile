﻿using System;

namespace Fatec.Core.Domain
{
	public class Student : AbstractAuditedEntity
	{
		public string Enrollment { get; set; }

		public string Fullname { get; set; }

		public DateTime BirthDate { get; set; }

		public string Period { get; set; }

		public DateTime EnrollDate { get; set; }

		public string SemesterAdmission { get; set; }

		public string RG { get; set; }

		public string HomePhone { get; set; }

		public string CellPhone { get; set; }

		public string Email { get; set; }

		public int CourseId { get; set; }

		public virtual Course Course { get; set; }
	}
}
