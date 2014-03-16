﻿using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Fatec.Repositories.SharePoint
{
	public class CourseRepository : ICourseRepository
	{
		private const string _path = "/fatec";
		private static readonly string[] _defaultViewFields = { "ID", "Title", "N_x00ed_vel_x0020_de_x0020_Ensin", "Perfil_x0020_Profissional", "Turnos", "Semestres", "Carga_x0020_Hor_x00e1_ria", "Ativo_x003f_" };

		private readonly ISPDbContext _context;

		public CourseRepository(ISPDbContext context)
		{
			_context = context;
		}

		public Course GetById(int id)
		{
			string query = string.Format("<Where><Eq><FieldRef Name='ID'/><Value Type='Text'>{0}</Value></Eq></Where>", id);
			string viewFields = _context.CreateViewFieldsNode(_defaultViewFields);
			return _context.ExecuteQuery<Course>(_path, "Cursos", query, viewFields, CourseMap.Map, 1).FirstOrDefault();
		}

		public ICollection<Course> GetAllActive()
		{
			string query = string.Format("<Where><Eq><FieldRef Name='Ativo_x003f_'/><Value Type='Text'>True</Value></Eq></Where>");
			string viewFields = _context.CreateViewFieldsNode(_defaultViewFields);
			return _context.ExecuteQuery<Course>(_path, "Cursos", query, viewFields, CourseMap.Map);
		}
	}
}