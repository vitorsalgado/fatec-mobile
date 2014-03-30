using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;

namespace Fatec.Repositories.SharePoint
{
	public class FatecRepository : IFatecRepository
	{
		private const string fatecListsPath = "/fatec";
		private const string ciListsPath = "/tic";
		private readonly ISPDbContext _context;

		public FatecRepository(ISPDbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		public ICollection<TeacherAbsence> GetTeacherAbsences()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Data'/><Value Type='DateTime'><Today /></Value></Geq>
					</Where><OrderBy><FieldRef Name='Data' Ascending='False'/></OrderBy>";
			string viewFields = _context.CreateViewFields("ID", "Title", "Data", "Professor", "Semestre", "Turno", "Disciplina", "Observa_x00e7__x00e3_o");

			return _context.ExecuteQuery<TeacherAbsence>(
				fatecListsPath, "Faltas", query, viewFields, FatecMap.MapTeacherAbsence);
		}

		public ICollection<Replacement> GetReplacements()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Data_x002f_Hora'/><Value Type='DateTime'><Today /></Value></Geq>
					</Where><OrderBy><FieldRef Name='Data_x002f_Hora' Ascending='False'/></OrderBy>";
			string viewFields = _context.CreateViewFields("ID", "Title", "Data_x002f_Hora", "Professor", "Disciplina", "Per_x00ed_odo");

			return _context.ExecuteQuery<Replacement>(
				fatecListsPath, "Reposições", query, viewFields, FatecMap.MapReplacement);
		}

		public ICollection<KeyMovement> GetKeyMovement()
		{
			var viewFields = _context.CreateViewFields(
				"ID", "Requisitante", "Data_x0020_de_x0020_Retirada", "Chave");

			string query = @"<Where>
						<IsNull><FieldRef Name='Data_x0020_de_x0020_Devolu_x00e7'/></IsNull>
					</Where><OrderBy><FieldRef Name='Requisitante' Ascending='True'/></OrderBy>";

			return _context.ExecuteQuery<KeyMovement>(
				ciListsPath, "Controle de Chaves", query, viewFields, FatecMap.MapKeyMovement);
		}
	}
}
