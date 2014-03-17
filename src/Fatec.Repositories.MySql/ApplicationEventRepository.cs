using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Fatec.Repositories.MySql
{
	public class ApplicationEventRepository : IApplicationEventsRepository
	{
		private DbContext _context;
		private IDbSet<ApplicationEvent> _logEventEntity;

		public ApplicationEventRepository(DbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");

			_context = context;
			_logEventEntity = _context.Set<ApplicationEvent>();
		}

		public void Save(ApplicationEvent log)
		{
			if (log == null) throw new ArgumentNullException("log");

			_logEventEntity.Add(log);
			_context.SaveChanges();
		}

		public ApplicationEvent GetById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id");

			return _logEventEntity.Find(id);
		}

		public ICollection<ApplicationEvent> GetAll()
		{
			return _logEventEntity.ToList();
		}
	}
}
