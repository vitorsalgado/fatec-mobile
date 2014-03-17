using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface IDisciplineService
	{
		Discipline GetById(int id);
		ICollection<Discipline> GetAllDisciplines();
	}
}
