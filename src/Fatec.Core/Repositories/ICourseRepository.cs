using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface ICourseRepository
	{
		ICollection<Course> GetAllActive();
		Course GetById(int id);
	}
}