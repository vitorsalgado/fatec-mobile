using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IStudentService
	{
		Student GetByEnrollment(string enrollment);
		ICollection<EnrolledDiscipline> GetEnrolledDisciplinesByEnrollment(string enrollment);
		ICollection<StudiesAdvance> GetStudiesAdvanceByEnrollment(string enrollment);
		ICollection<Exam> GetExamsByEnrollment(string enrollment);
		ICollection<Requirement> GetRequirements(string enrollment);
	}
}
