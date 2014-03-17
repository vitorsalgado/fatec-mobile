using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IClassAssignmentRepository
	{
		Discipline GetDisciplineById(int id);
		ICollection<Discipline> GetAllDisciplines();
	}
}
