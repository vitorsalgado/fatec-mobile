using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;

namespace Fatec.Repositories.MySql
{
	public class LogRepository : ILogRepository
	{
		private DbContext _context;
		private IDbSet<Log> _logEntity;

		public LogRepository(DbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");

			_context = context;
			_logEntity = _context.Set<Log>();
		}

		public void Save(Log log)
		{
			if (log == null) throw new ArgumentNullException("log");
			_logEntity.Add(log);
			_context.SaveChanges();
		}

		public Log GetById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id");
			return _logEntity.Find(id);
		}

		public ICollection<Log> GetAll()
		{
			return _logEntity.ToList();
		}

		public ICollection<Log> Find(LogCriteria logCriteria)
		{
			if (logCriteria == null) throw new ArgumentNullException("logCriteria");

			var query = _logEntity.AsQueryable();

			if (logCriteria.Id.HasValue)
				query.Where(x => x.Id == logCriteria.Id.Value);

			return query
				.OrderBy(logCriteria.SortExpression)
				.ToList();
		}
	}
}
