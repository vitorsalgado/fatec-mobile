using Fatec.Repositories.MySql.Mapping;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace Fatec.Repositories.MySql
{
	public class FatecDbContext : DbContext
	{
		public FatecDbContext(string nameOrConnectionString) : base(nameOrConnectionString) { }
		public FatecDbContext() : this("fatec-mobile") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			Type configType = typeof(LogMap);

			var typesToRegister = Assembly.GetAssembly(configType).GetTypes()
				.Where(type =>
					type.BaseType != null
					&& type.BaseType.IsGenericType
					&& (type.BaseType.GetGenericTypeDefinition() == typeof(AbstractMap<>) || type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
					&& !type.IsAbstract);

			foreach (var type in typesToRegister)
			{
				dynamic configurationInstance = Activator.CreateInstance(type);
				modelBuilder.Configurations.Add(configurationInstance);
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
