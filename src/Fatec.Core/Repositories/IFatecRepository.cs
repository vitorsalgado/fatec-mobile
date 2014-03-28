using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IFatecRepository
	{
		ICollection<TeacherAbsence> GetTeacherAbsences();
		ICollection<ClassReplacement> GetReplacements();
	}
}
