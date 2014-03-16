using Fatec.Core.Domain;

namespace Fatec.Repositories.MySql.Mapping
{
	public class LogInformationMap : AbstractMap<ApplicationActionLog>
	{
		public LogInformationMap()
		{
			this.Property(x => x.ActionType).IsRequired();
			this.Property(x => x.Date).IsRequired();
			this.Property(x => x.Message).IsRequired().IsMaxLength();

			this.ToTable("log_information");
		}
	}
}
