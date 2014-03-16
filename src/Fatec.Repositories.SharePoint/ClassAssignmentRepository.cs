using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System.Linq;

namespace Fatec.Repositories.SharePoint
{
	public class ClassAssignmentRepository : IClassAssignmentRepository
	{
		private readonly ISPDbContext _context;

		public ClassAssignmentRepository(ISPDbContext context)
		{
			_context = context;
		}
		public Discipline GetDisciplineById(int id)
		{
			string query = string.Format(@"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>{0}</Value></Eq></Where>", id);
			string viewFields = _context.CreateViewFieldsNode("Title", "C_x00ed_clo");

			return _context.ExecuteQuery<Discipline>("/fatec", "Disciplinas", query, viewFields, ClassAssignmentMap.MapDiscipline, 1).FirstOrDefault();
		}
	}
}
