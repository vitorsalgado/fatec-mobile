using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	public class DisciplineMap : BaseMapper
	{
		public static Func<XElement, Discipline> Map = xElement =>
		{
			var discipline = new Discipline();

			discipline.Cycle = xElement.GetAttrValue<string>("ows_C_x00ed_clo");
			discipline.Acronym = xElement.GetAttrValue<string>("ows_Sigla");
			discipline.Name = xElement.GetAttrValue<string>("ows_Title");
			discipline.Workload = xElement.GetAttrValue<decimal>("ows_Carga_x0020_Hor_x00e1_ria_x0020_");
			discipline.TotalWorkload = xElement.GetAttrValue<decimal>("ows_Carga_x0020_Hor_x00e1_ria_x0020_0");
			discipline.Credits = xElement.GetAttrValue<decimal>("ows_Cr_x00e9_ditos");

			FillDefaultFields(discipline, xElement);

			return discipline;
		};
	}
}
