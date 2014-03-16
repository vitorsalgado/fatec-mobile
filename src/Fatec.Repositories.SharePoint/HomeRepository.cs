using Fatec.Core.Repositories;

namespace Fatec.Repositories.SharePoint
{
	public class HomeRepository : BaseAnnouncementsRepository, IHomeAnnouncementsRepository
	{
		public HomeRepository(ISPDbContext context)
			: base(context) { }

		protected override string AnnouncementsListPath
		{
			get { return "/"; }
		}

		protected override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
