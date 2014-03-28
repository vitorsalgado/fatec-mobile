using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IStudentService
	{
		Student Get(string enrollment);
		ICollection<EnrolledDiscipline> GetEnrolledDisciplines(string enrollment);
		ICollection<StudiesAdvance> GetStudiesAdvance(string enrollment);
		ICollection<Exam> GetExams(string enrollment);
		ICollection<Requirement> GetRequirements(string enrollment);
	}
}
