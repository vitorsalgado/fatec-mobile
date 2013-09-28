using Fatec.Core.Repositories;

namespace Fatec.Repository.SharePoint
{
	public class TIAnnouncementsRepository : BaseAnnouncementsRepository, ITIAnnouncementsRepository
	{
		protected override string AnnouncementsListPath
		{
			get { return "/tic"; }
		}

		public override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
