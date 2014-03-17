using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface IApplicationEventsRepository
	{
		void Save(ApplicationEvent log);
		ApplicationEvent GetById(int id);
		ICollection<ApplicationEvent> GetAll();
	}
}
