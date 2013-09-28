using Fatec.Core.Repositories;

namespace Fatec.Repository.SharePoint
{
	public class TraineeRepository : BaseAnnouncementsRepository, ITraineeRepository
	{
		protected override string AnnouncementsListPath
		{
			get { return "/estagio"; }
		}

		public override string AnnouncementsListName
		{
			get { return "Oportunidades"; }
		}
	}
}
