using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatec.Repositories.SharePoint
{
	public class StudentRepository : IStudentRepository
	{
		private static readonly string[] _studentsDefaultViewFields = { "Nome_x0020_Completo", "Curso", "Turno", "Registro_x0020_Geral", "Org_x00e3_o_x0020_Emissor_x0020_", "Data_x0020_de_x0020_Emiss_x00e3_", "Data_x0020_de_x0020_Nascimento", "Naturalidade", "Endere_x00e7_o", "Bairro", "Munic_x00ed_pio_x0020_da_x0020_R", "Estado_x0020_de_x0020_Resid_x00e", "Telefone_x0020__x0028_res_x0029_", "Celular", "Email", "Turma_x0020_de_x0020_Ingresso", "Data_x0020_da_x0020_Matr_x00ed_c", "Observa_x00e7__x00f5_es_x0020_pa" };
		private const string LISTS_SERVICE_RELATIVE_PATH = "/fatec";
		private readonly ISPDbContext _context;

		public StudentRepository(ISPDbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		public ICollection<EnrolledDiscipline> GetEnrolledDisciplines(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment))
				throw new ArgumentNullException("enrollment");

			var query = string.Format("<Where><Eq><FieldRef Name='Matr_x00ed_cula'/><Value Type='Text'>{0}</Value></Eq></Where>", enrollment);
			var viewFields = _context.CreateViewFields(
				"Disciplina", "Matr_x00ed_cula", "Situa_x00e7__x00e3_o", "Hist_x00f3_rico", "Turno", "Nota_x0020_1", "Nota_x0020_2",
				"Faltas_x0020__x0028_1_x00ba__x00", "Faltas_x0020__x0028_2_x00ba__x00", "Faltas", "M_x00e9_dia", "Conceito", "NP", "Author", "Created");

			return _context.ExecuteQuery<EnrolledDiscipline>(
				LISTS_SERVICE_RELATIVE_PATH, "Disciplinas Matriculadas no Semestre", query, viewFields, StudentMap.MapEnrolledDisciplines);
		}

		public Student Get(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment))
				throw new ArgumentNullException("enrollment");

			var query = string.Format(@"<Where><Eq><FieldRef Name='Title'/><Value Type='Text'>{0}</Value></Eq></Where>", enrollment.Trim().ToUpperInvariant());
			var viewFields = _context.CreateViewFields(_studentsDefaultViewFields);

			return _context.ExecuteQuery<Student>(
				LISTS_SERVICE_RELATIVE_PATH, "Matrículas", query, viewFields, StudentMap.Map, 1).FirstOrDefault();
		}

		public ICollection<Student> GetAll()
		{
			var viewFields = _context.CreateViewFields(_studentsDefaultViewFields);

			return _context.ExecuteQuery<Student>(
				LISTS_SERVICE_RELATIVE_PATH, "Matrículas", string.Empty, viewFields, StudentMap.Map);
		}

		public ICollection<StudiesAdvance> GetStudiesAdvance(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment))
				throw new ArgumentNullException("enrollment");

			string query = string.Format(@"<Where><Eq>
				<FieldRef Name='Matr_x00ed_cula'/><Value Type='Text'>{0}</Value></Eq></Where>
				<OrderBy><FieldRef Name='Disciplina' Ascending=""True""/></OrderBy>", enrollment);

			string viewFields = _context.CreateViewFields("Semestre", "Disciplina", "Turno", "Situa_x00e7__x00e3_o", "Parecer_x0020_do_x0020_Professor");

			return _context.ExecuteQuery<StudiesAdvance>(
				LISTS_SERVICE_RELATIVE_PATH, "Aproveitamento de Estudos", query, viewFields, StudentMap.MapStudiesAdvance);
		}

		public ICollection<Exam> GetExams(string enrollment)
		{
			bool canContinue = true;
			StringBuilder query = new StringBuilder("<Where>");

			var disciplines = this.GetEnrolledDisciplines(enrollment).ToArray();
			if (disciplines.Count() == 0)
				return new List<Exam>();

			for (int i = 0; i < disciplines.Length; i++)
			{
				var discipline = disciplines[i];

				if (canContinue)
				{
					canContinue = false;

					query.Append("<And>");

					for (var x = 0; x < disciplines.Length - 1; x++)
						query.Append("<Or>");

					query
						.Append("<And>")
							.Append("<Eq><FieldRef Name='Disciplina' LookupId='True'/><Value Type='Lookup'>").Append(discipline.Id).Append("</Value></Eq>")
							.Append("<Eq><FieldRef Name='Turno'/><Value Type='Text'>").Append(discipline.Period).Append("</Value></Eq>")
						.Append("</And>");
				}
				else
				{
					query
						.Append("<And>")
							.Append("<Eq><FieldRef Name='Disciplina' LookupId='True'/><Value Type='Lookup'>").Append(discipline.Id).Append("</Value></Eq>")
							.Append("<Eq><FieldRef Name='Turno'/><Value Type='Text'>").Append(discipline.Period).Append("</Value></Eq>")
						.Append("</And>")
					.Append("</Or>");
				}
			}

			query.Append("<Eq><FieldRef Name='Vigente_x003f_'/><Value Type='Integer'>1</Value></Eq>")
				.Append("</And>")
			.Append("</Where>")
			.Append("<OrderBy><FieldRef Name='Disciplina' Ascending='True'/></OrderBy>");

			var viewFields = _context.CreateViewFields("ID", "Professor", "Disciplina", "Turno", "Data_x0020_P1", "Data_x0020_da_x0020_P2", "Sala_x0020_de_x0020_Aula", "Hor_x00e1_rio_x0028_s_x0029_");

			return _context.ExecuteQuery<Exam>(
				LISTS_SERVICE_RELATIVE_PATH, "Atribuição de Aulas à Professores", query.ToString(), viewFields, StudentMap.MapExam);
		}

		public ICollection<Requirement> GetRequirements(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment))
				throw new ArgumentNullException("enrollment");

			string query = string.Format(@"<Where><Contains>
				<FieldRef Name='Author'/><Value Type='Text'>{0}</Value></Contains></Where>
				<OrderBy><FieldRef Name='Title' Ascending=""True""/></OrderBy>", enrollment);

			string viewFields = _context.CreateViewFields("Category", "V3Comments", "Title", "DueDate", "Resultado", "Author");

			return _context.ExecuteQuery<Requirement>(
				LISTS_SERVICE_RELATIVE_PATH, "Requerimentos ON-LINE", query, viewFields, StudentMap.MapRequirement);
		}

		//public Timetable GetTimetableByEnrollment(string enrollment)
		//{
		//	bool canContinue = true;
		//	StringBuilder query = new StringBuilder("<Where>");

		//	var disciplines = this.GetEnrolledDisciplinesByEnrollment(enrollment).ToArray();
		//	if (disciplines.Count() == 0)
		//		return null;

		//	for (int i = 0; i < disciplines.Length; i++)
		//	{
		//		var discipline = disciplines[i];

		//		if (canContinue)
		//		{
		//			canContinue = false;

		//			query.Append("<And>");

		//			for (var x = 0; x < disciplines.Length - 1; x++)
		//				query.Append("<Or>");

		//			query
		//				.Append("<And>")
		//					.Append("<Eq><FieldRef Name='Disciplina' LookupId='True'/><Value Type='Lookup'>").Append(discipline.Id).Append("</Value></Eq>")
		//					.Append("<Eq><FieldRef Name='Turno'/><Value Type='Text'>").Append(discipline.Period).Append("</Value></Eq>")
		//				.Append("</And>");
		//		}
		//		else
		//		{
		//			query
		//				.Append("<And>")
		//					.Append("<Eq><FieldRef Name='Disciplina' LookupId='True'/><Value Type='Lookup'>").Append(discipline.Id).Append("</Value></Eq>")
		//					.Append("<Eq><FieldRef Name='Turno'/><Value Type='Text'>").Append(discipline.Period).Append("</Value></Eq>")
		//				.Append("</And>")
		//			.Append("</Or>");
		//		}
		//	}

		//	query.Append("<Eq><FieldRef Name='Vigente_x003f_'/><Value Type='Integer'>1</Value></Eq>")
		//		.Append("</And>")
		//	.Append("</Where>")
		//	.Append("<OrderBy><FieldRef Name='Disciplina' Ascending='True'/></OrderBy>");

		//	var viewFields = SPDb.CreateViewFieldsNode("ID", "Professor", "Disciplina", "Turno", "Data_x0020_P1", "Data_x0020_da_x0020_P2", "Sala_x0020_de_x0020_Aula", "Hor_x00e1_rio_x0028_s_x0029_");

		//	var classAssignments = SPDb.ExecuteQuery<ClassAssignmentDto>(LISTS_SERVICE_RELATIVE_PATH, "Atribuição de Aulas à Professores", query.ToString(), viewFields, StudentMap.MapClassAssigment)
		//		.AsQueryable();

		//	Timetable timetable = new Timetable();

		//	var timetableQuery =	from classAssignment in classAssignments
		//							group classAssignment by classAssignment.Period into byPeriod
		//							select new
		//							{
		//								Period = byPeriod.Key,
		//								Schedules = from schedule in byPeriod
		//											group schedule by schedule.Schedule.Substring(
		//							};
		//}
	}
}