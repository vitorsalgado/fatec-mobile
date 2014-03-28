using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IDisciplineRepository
	{
		Discipline GetById(int id);
		ICollection<Discipline> GetAll();
	}
}
