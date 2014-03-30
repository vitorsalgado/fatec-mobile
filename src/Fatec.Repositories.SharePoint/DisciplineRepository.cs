using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fatec.Repositories.SharePoint
{
	public class DisciplineRepository : IDisciplineRepository
	{
		private const string _listPath = "/fatec";
		private const string _listName = "Disciplinas";
		private static readonly string[] _viewFields = { "Title", "C_x00ed_clo", "ows_Cr_x00e9_ditos", "ows_Carga_x0020_Hor_x00e1_ria_x0020_0", "ows_Carga_x0020_Hor_x00e1_ria_x0020_" };

		private readonly ISPDbContext _context;

		public DisciplineRepository(ISPDbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		public Discipline GetById(int id)
		{
			string query = string.Format(@"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>{0}</Value></Eq></Where>", id);
			string viewFields = _context.CreateViewFields(_viewFields);

			return _context.ExecuteQuery<Discipline>(
				_listPath, _listName, query, viewFields, DisciplineMap.Map, 1).FirstOrDefault();
		}

		public ICollection<Discipline> GetAll()
		{
			string query = @"<Where><Eq><FieldRef Name='Ativa_x003f_' /><Value Type='Text'>True</Value></Eq></Where>";
			string viewFields = _context.CreateViewFields(_viewFields);

			return _context.ExecuteQuery<Discipline>(
				_listPath, _listName, query, viewFields, DisciplineMap.Map);
		}
	}
}
