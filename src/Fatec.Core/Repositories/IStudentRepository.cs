using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IStudentRepository
	{
		ICollection<EnrolledDiscipline> GetEnrolledDisciplines(string enrollment);
		Student Get(string enrollment);
		ICollection<Student> GetAll();
		ICollection<StudiesAdvance> GetStudiesAdvance(string enrollment);
		ICollection<Exam> GetExams(string enrollment);
		ICollection<Requirement> GetRequirements(string enrollment);
		History GetHistory(string enrollment);
	}
}
