using Fatec.Core.Repositories;

namespace Fatec.Repositories.SharePoint
{
	public class TIAnnouncementsRepository : BaseAnnouncementsRepository, ITIAnnouncementsRepository
	{
		public TIAnnouncementsRepository(ISPDbContext context)
			: base(context) { }

		protected override string AnnouncementsListPath
		{
			get { return "/tic"; }
		}

		protected override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
