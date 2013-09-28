using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repository.Mapping;
using System.Linq;

namespace Fatec.Repository.SharePoint
{
	public class ClassAssignmentRepository : IClassAssignmentRepository
	{ 
		public Discipline GetDisciplineById(int id)
		{
			string query = string.Format(@"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>{0}</Value></Eq></Where>", id);
			string viewFields = SPDb.CreateViewFieldsNode("Title", "C_x00ed_clo");

			return SPDb.ExecuteQuery<Discipline>("/fatec", "Disciplinas", query, viewFields, ClassAssignmentMap.MapDiscipline, 1).FirstOrDefault();
		}
	}
}
