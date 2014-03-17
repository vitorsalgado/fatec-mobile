using Fatec.Core.Domain;

namespace Fatec.Repositories.MySql.Mapping
{
	public class ApplicationEventMap : AbstractMap<ApplicationEvent>
	{
		public ApplicationEventMap()
		{
			this.Property(x => x.EventType).HasMaxLength(100).IsRequired();
			this.Property(x => x.Date).IsRequired();
			this.Property(x => x.Event).IsRequired().IsMaxLength();

			this.ToTable("log_information");
		}
	}
}
