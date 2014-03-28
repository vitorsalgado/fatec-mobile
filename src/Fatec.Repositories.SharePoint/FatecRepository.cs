﻿using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;

namespace Fatec.Repositories.SharePoint
{
	public class FatecRepository : IFatecRepository
	{
		private const string FATEC_LISTS_SERVICE_PATH = "/fatec";
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
				FATEC_LISTS_SERVICE_PATH, "Faltas", query, viewFields, FatecMap.MapTeacherAbsence);
		}

		public ICollection<ClassReplacement> GetReplacements()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Data_x002f_Hora'/><Value Type='DateTime'><Today /></Value></Geq>
					</Where><OrderBy><FieldRef Name='Data_x002f_Hora' Ascending='False'/></OrderBy>";
			string viewFields = _context.CreateViewFields("ID", "Title", "Data_x002f_Hora", "Professor", "Disciplina", "Per_x00ed_odo");

			return _context.ExecuteQuery<ClassReplacement>(
				FATEC_LISTS_SERVICE_PATH, "Reposições", query, viewFields, FatecMap.MapReplacement);
		}
	}
}
