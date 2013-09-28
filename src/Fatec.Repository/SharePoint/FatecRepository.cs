using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repository.Mapping;
using System.Collections.Generic;

namespace Fatec.Repository.SharePoint
{
	public class FatecRepository : BaseAnnouncementsRepository, IFatecRepository
	{
		private const string FATEC_LISTS_SERVICE_PATH = "/fatec";

		protected override string AnnouncementsListPath
		{
			get { return FATEC_LISTS_SERVICE_PATH; }
		}

		public override string AnnouncementsListName
		{
			get { return "Avisos"; }
		}

		public ICollection<TeacherAbsence> GetTeacherAbsences()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Data'/><Value Type='DateTime'><Today /></Value></Geq>
					</Where><OrderBy><FieldRef Name='Data' Ascending='False'/></OrderBy>";
			string viewFields = SPDb.CreateViewFieldsNode("ID", "Title", "Data", "Professor", "Semestre", "Turno", "Disciplina", "Observa_x00e7__x00e3_o");

			return SPDb.ExecuteQuery<TeacherAbsence>(FATEC_LISTS_SERVICE_PATH, "Faltas", query, viewFields, FatecMap.MapTeacherAbsence);
		}

		public ICollection<ClassReplacement> GetVigentClassReplacements()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Data_x002f_Hora'/><Value Type='DateTime'><Today /></Value></Geq>
					</Where><OrderBy><FieldRef Name='Data_x002f_Hora' Ascending='False'/></OrderBy>";
			string viewFields = SPDb.CreateViewFieldsNode("ID", "Title", "Data_x002f_Hora", "Professor", "Disciplina", "Per_x00ed_odo");

			return SPDb.ExecuteQuery<ClassReplacement>(FATEC_LISTS_SERVICE_PATH, "Reposições", query, viewFields, FatecMap.MapClassReplacement);
		}

		public Announcement GetAnnouncementById(int id)
		{
			return base.Get(id);
		}

		public ICollection<Announcement> GetVigentAnnouncements()
		{
			return base.GetAllValid();
		}
	}
}
