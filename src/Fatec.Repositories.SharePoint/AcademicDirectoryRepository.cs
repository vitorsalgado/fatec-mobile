using Fatec.Core.Repositories;

namespace Fatec.Repositories.SharePoint
{
	public class DiretorioAcademicoRepository : BaseAnnouncementsRepository, IAcademicDirectoryAnnouncementsRepository
	{
		public DiretorioAcademicoRepository(ISPDbContext context)
			: base(context) { }

		protected override string AnnouncementsListPath
		{
			get { return "/da"; }
		}

		protected override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}
	}
}
