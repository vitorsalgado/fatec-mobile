using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	internal class ClassAssignmentMap : BaseMapper
	{
		internal static Func<XElement, Discipline> MapDiscipline = xElement =>
		{
			var disciplina = new Discipline();

			disciplina.Cycle = xElement.GetAttrValue<string>("ows_C_x00ed_clo");
			disciplina.Acronym = xElement.GetAttrValue<string>("ows_Sigla");
			disciplina.Name = xElement.GetAttrValue<string>("ows_Title");

			FillDefaultFields(disciplina, xElement);

			return disciplina;
		};
	}
}
