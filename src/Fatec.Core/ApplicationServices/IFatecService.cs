using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IFatecService
	{
		ICollection<Course> GetActivesCourses();
		Course GetCourseById(int id);
		ICollection<TeacherAbsence> GetTeachersAbsences();
		ICollection<ClassReplacement> GetClassReplacements();
	}
}
