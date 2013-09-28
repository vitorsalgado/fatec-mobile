using Fatec.Core.Repositories;

namespace Fatec.Repository.SharePoint
{
	public class HomeRepository : BaseAnnouncementsRepository, IHomeAnnouncementsRepository
	{
		protected override string AnnouncementsListPath
		{
			get { return "/"; }
		}

		public override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
