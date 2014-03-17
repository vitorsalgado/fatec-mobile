using System;
using System.Data.Entity;

namespace Fatec.Repositories.MySql.Core
{
	public class DbInit<T> : IDatabaseInitializer<T> where T : DbContext
	{
		public void InitializeDatabase(T context)
		{
			if (context == null) throw new ArgumentNullException("context");

			bool databaseExists = context.Database.Exists();
			if (databaseExists)
			{
				if (context.Database.CompatibleWithModel(false))
					return;
			}

			context.Database.Delete();
			context.Database.Create();

			context.SaveChanges();
		}
	}
}
