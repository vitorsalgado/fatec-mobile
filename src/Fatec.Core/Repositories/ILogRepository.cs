using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface ILogRepository
	{
		void Save(Log log);
		Log GetById(int id);
		ICollection<Log> GetAll();
		ICollection<Log> Find(LogCriteria logCriteria);
	}
}
