using Fatec.Core.Domain;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	internal class CourseMap : BaseMapper
	{
		internal static Func<XElement, Course> Map = xElement =>
		{
			var curso = new Course();
			curso.EducationLevel = xElement.GetAttrValue<string>("ows_N_x00ed_vel_x0020_de_x0020_Ensin");
			curso.Name = xElement.GetAttrValue<string>("ows_Title");
			curso.TeachingProfile = xElement.GetAttrValue<string>("ows_Perfil_x0020_Profissional");
			curso.WorkLoad = Convert.ToInt32(xElement.GetAttrValue<decimal>("ows_Carga_x0020_Hor_x00e1_ria"));
			curso.DurationInMonths = Convert.ToInt32(xElement.GetAttrValue<decimal>("ows_Semestres"));

			var turnos = xElement.GetAttrValue<string>("ows_Turnos");
			curso.Periods = FormatPeriod(turnos);

			FillDefaultFields(curso, xElement);
			return curso;
		};
	}
}
