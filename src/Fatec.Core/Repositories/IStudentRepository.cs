using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IStudentRepository
	{
		ICollection<EnrolledDiscipline> GetEnrolledDisciplinesByEnrollment(string enrollment);
		Student GetByEnrollment(string enrollment);
		ICollection<Student> GetAll();
		ICollection<StudiesAdvance> GetStudiesAdvanceByEnrollment(string enrollment);
		ICollection<Exam> GetExamsByEnrollment(string enrollment);
		ICollection<Requirement> GetRequirementsByEnrollment(string enrollment);
	}
}
