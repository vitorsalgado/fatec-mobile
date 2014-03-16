using Fatec.Core.Domain;
using Fatec.Repositories.Dtos;
using Fatec.Repositories.Proxies;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	internal class StudentMap : BaseMapper
	{
		internal static Func<XElement, StudiesAdvance> MapStudiesAdvance = xElement =>
		{
			var result = new StudiesAdvance();

			result.Period = xElement.GetAttrValue<string>("ows_Turno");
			result.Situation = xElement.GetAttrValue<string>("ows_Situa_x00e7__x00e3_o");
			result.Semester = xElement.GetAttrValue<string>("ows_Semestre");

			string[] disciplineArray = xElement.GetAttrValue<string>("ows_Disciplina").Split(';', '#');
			result.DisciplineId = Convert.ToInt32(disciplineArray[0]);

			result.TeacherDecision = xElement.GetAttrValue<string>("ows_Parecer_x0020_do_x0020_Professor");

			FillDefaultFields(result, xElement);

			return result;
		};

		internal static Func<XElement, Student> Map = xElement =>
		{
			var result = new StudentProxy();

			result.Enrollment = xElement.GetAttrValue<string>("ows_Title");
			result.BirthDate = xElement.GetAttrValue<DateTime>("ows_Data_x0020_de_x0020_Nascimento");
			result.CellPhone = xElement.GetAttrValue<string>("ows_Celular");
			result.CourseId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Curso").Split(';')[0]);
			result.EnrollDate = xElement.GetAttrValue<DateTime>("ows_Data_x0020_da_x0020_Matr_x00ed_c");
			result.Email = xElement.GetAttrValue<string>("ows_Email");
			result.Period = xElement.GetAttrValue<string>("ows_Turno");

			FillDefaultFields(result, xElement);

			return result;
		};

		internal static Func<XElement, EnrolledDiscipline> MapEnrolledDisciplines = xElement =>
		{
			var result = new EnrolledDiscipline();

			result.DisciplineId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Disciplina").Split(';')[0]);
			result.History = xElement.GetAttrValue<string>("ows_Hist_x00f3_rico");
			result.Registry = xElement.GetAttrValue<string>("ows_Matr_x00ed_cula");
			result.Period = xElement.GetAttrValue<string>("ows_Turno");
			result.GradeP1 = xElement.GetAttrValue<decimal>("ows_Nota_x0020_1");
			result.GradeP2 = xElement.GetAttrValue<decimal>("ows_Nota_x0020_2");
			result.AbsencesFirstTwoMonths = xElement.GetAttrValue<int>("ows_Faltas_x0020__x0028_1_x00ba__x00");
			result.AbsencesSecondTwoMonths = xElement.GetAttrValue<int>("ows_Faltas_x0020__x0028_2_x00ba__x00");
			result.AbsencesPercent = xElement.GetAttrValue<decimal>("ows_Faltas");
			result.WorkGrade = xElement.GetAttrValue<decimal>("ows_NP");
			result.Grade = xElement.GetAttrValue<decimal>("ows_M_x00e9_dia");
			result.Concept = xElement.GetAttrValue<string>("ows_Conceito").Split('#')[1];

			FillDefaultFields(result, xElement);

			return result;
		};

		internal static Func<XElement, Exam> MapExam = xElement =>
		{
			var exam = new Exam();

			exam.Professor = xElement.GetAttrValue<string>("ows_Professor").Split('#')[1];
			exam.FirstExamDate = xElement.GetAttrValue<DateTime>("ows_Data_x0020_P1");
			exam.SecondExamDate = xElement.GetAttrValue<DateTime>("ows_Data_x0020_da_x0020_P2");
			exam.Period = xElement.GetAttrValue<string>("ows_Turno");
			exam.DisciplineId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Disciplina").Split(';')[0]);

			FillDefaultFields(exam, xElement);

			return exam;
		};

		internal static Func<XElement, Requirement> MapRequirement = xElement =>
		{
			var requirement = new Requirement();

			requirement.Category = xElement.GetAttrValue<string>("ows_Category");
			requirement.Comments = xElement.GetAttrValue<string>("ows_V3Comments");
			requirement.Description = xElement.GetAttrValue<string>("ows_Title");
			requirement.EndDate = xElement.GetAttrValue<DateTime>("ows_DueDate");
			requirement.Result = xElement.GetAttrValue<string>("ows_Resultado");

			FillDefaultFields(requirement, xElement);

			return requirement;
		};

		internal static Func<XElement, ClassAssignmentDto> MapClassAssigment = xElement =>
		{
			var classAssigment = new ClassAssignmentDto();

			classAssigment.TeacherName = xElement.GetAttrValue<string>("ows_Professor").Split('#')[1];
			classAssigment.Schedule = xElement.GetAttrValue<string>("ows_Hor_x00e1_rio_x0028_s_x0029_");
			classAssigment.ClassRoom = xElement.GetAttrValue<string>("ows_Sala_x0020_de_x0020_Aula");
			classAssigment.Period = xElement.GetAttrValue<string>("ows_Turno");
			classAssigment.Semester = xElement.GetAttrValue<string>("ows_Semestre");
			classAssigment.DisciplineId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Disciplina").Split(';')[0]);

			return classAssigment;
		};
	}
}
