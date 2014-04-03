using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.SharePoint.Mapping
{
	public static class HistoryEntryMap
	{
		public static Func<XElement, HistoryEntry> Map = xElement =>
		{
			HistoryEntry historyEntry = new HistoryEntry();
			historyEntry.Discipline = new Discipline();

			string[] disciplineArray = xElement.GetAttrValue<string>("ows_Disciplina").Split(new char[] { ';', '#'}, StringSplitOptions.RemoveEmptyEntries);
			string[] semesterArray = xElement.GetAttrValue<string>("ows_Semestre").Split(new char[] { ';', '#' }, StringSplitOptions.RemoveEmptyEntries);

			historyEntry.Discipline.Id = Convert.ToInt32(disciplineArray[0]);
			historyEntry.Average = xElement.GetAttrValue<decimal>("ows_M_x00e9_dia");
			historyEntry.Concept = xElement.GetAttrValue<string>("ows_Conceito");
			historyEntry.Period = xElement.GetAttrValue<string>("ows_Turno");
			historyEntry.Semester = semesterArray[1];

			return historyEntry;
		};
	}
}
