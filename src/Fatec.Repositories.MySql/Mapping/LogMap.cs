using Fatec.Core.Domain;

namespace Fatec.Repositories.MySql.Mapping
{
	public class LogMap : AbstractMap<Log>
	{
		public LogMap()
		{
			this.Property(x => x.CreatedOn).IsRequired();
			this.Property(x => x.Details).IsMaxLength();
			this.Property(x => x.IpAddress);
			this.Property(x => x.Message).IsRequired().IsMaxLength();
			this.Property(x => x.Url).IsMaxLength();
			this.Property(x => x.Username).HasMaxLength(250);

			this.ToTable("log");
		}
	}
}
