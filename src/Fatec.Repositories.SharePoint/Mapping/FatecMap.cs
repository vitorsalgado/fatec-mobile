using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	public class FatecMap : BaseMapper
	{
		public static Func<XElement, TeacherAbsence> MapTeacherAbsence = xElement =>
		{
			var result = new TeacherAbsence();

			result.Date = xElement.GetAttrValue<DateTime>("ows_Data");
			result.Reason = xElement.GetAttrValue<string>("ows_Motivo");
			result.Observations = xElement.GetAttrValue<string>("ows_Observa_x00e7__x00e3_o");

			var teacherName = xElement.GetAttrValue<string>("ows_Professor");
			if(!string.IsNullOrWhiteSpace(teacherName))
				result.TeacherName = xElement.GetAttrValue<string>("ows_Professor").Split('#')[1];

			result.Semester = xElement.GetAttrValue<string>("ows_Semestre");
			
			var turnos = xElement.GetAttrValue<string>("ows_Turno");
			result.Periods = FormatPeriod(turnos);

			result.DisciplineId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Disciplina").Split(';')[0]);

			FillDefaultFields(result, xElement);

			return result;
		};

		public static Func<XElement, Replacement> MapReplacement = xElement =>
		{
			var reposicao = new Replacement();

			reposicao.Date = xElement.GetAttrValue<DateTime>("ows_Data_x002f_Hora");
			reposicao.DisciplineId = Convert.ToInt32(xElement.GetAttrValue<string>("ows_Disciplina").Split(';')[0]);
			reposicao.TeacherName = xElement.GetAttrValue<string>("ows_Professor").Split('#')[1];

			var turnos = xElement.GetAttrValue<string>("ows_Per_x00ed_odo");
			reposicao.Periods = FormatPeriod(turnos);

			FillDefaultFields(reposicao, xElement);

			return reposicao;
		};
	}
}
