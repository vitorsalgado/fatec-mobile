using Fatec.Core.Infrastructure.Tasks;
using System.Data.Entity;

namespace Fatec.Repositories.MySql.Core
{
	public class EfTask : ITask
	{
		public string Description
		{
			get { return "Task to start mysql database."; }
		}

		public void Run()
		{
			Database.SetInitializer<FatecDbContext>(new DbInit<FatecDbContext>());

			using (var startContext = new FatecDbContext())
				startContext.Database.Initialize(true);
		}
	}
}
