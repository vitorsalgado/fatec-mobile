using Fatec.Core.Repositories;

namespace Fatec.Repository.SharePoint
{
	public class DiretorioAcademicoRepository : BaseAnnouncementsRepository, IAcademicDirectoryAnnouncementsRepository
	{
		protected override string AnnouncementsListPath
		{
			get { return "/da"; }
		}

		public override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
