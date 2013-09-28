using Fatec.Core.Domain;

namespace Fatec.Core.Repositories
{
	public interface IClassAssignmentRepository
	{
		Discipline GetDisciplineById(int id);
	}
}
