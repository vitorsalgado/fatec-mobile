using Fatec.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Fatec.Repositories.MySql.Mapping
{
	public abstract class AbstractMap<T> : EntityTypeConfiguration<T> where T : AbstractEntity
	{
		protected AbstractMap()
		{
			this.HasKey(x => x.Id);
			this.Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		}
	}
}
