using Fatec.Core.Domain;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Fatec.Repository.Mapping
{
	public abstract class BaseMapper
	{
		protected static void FillDefaultFields(AuditedEntity entity, XElement xElement)
		{
			entity.Id = Convert.ToInt32(xElement.GetAttrValue<int>("ows_ID"));

			var createdOnFieldValue = xElement.GetAttrValue<string>("ows_Created");
			if (!String.IsNullOrWhiteSpace(createdOnFieldValue))
				entity.CreatedOn = Convert.ToDateTime(createdOnFieldValue);

			var creatorFieldValue = xElement.GetAttrValue<string>("ows_Author");
			if (!String.IsNullOrWhiteSpace(creatorFieldValue))
			{
				var splitedValue = creatorFieldValue.Split(new char[] { ';', '#' });
				entity.CreatorId = Convert.ToInt32(splitedValue[0]);
				entity.CreatedBy = splitedValue[2].RemoveDomain();
			}
		}

		protected static IEnumerable<string> FormatPeriod(string turnos)
		{
			IEnumerable<string> turnosCollection = turnos.Substring(2, turnos.Length - 2).Split(new char[] { ';', '#' });
			return turnosCollection;
		}
	}
}
