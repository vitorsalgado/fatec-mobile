using Fatec.Core.Repositories;

namespace Fatec.Repositories.SharePoint
{
	public class TraineeRepository : BaseAnnouncementsRepository, ITraineeRepository
	{
		public TraineeRepository(ISPDbContext context)
			: base(context) { }

		protected override string AnnouncementsListPath
		{
			get { return "/estagio"; }
		}

		protected override string AnnouncementsListName
		{
			get { return "Oportunidades"; }
		}
	}
}
