using Fatec.Core.Domain;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	public abstract class BaseMapper
	{
		protected static void FillDefaultFields(AbstractAuditedEntity entity, XElement xElement)
		{
			if (entity == null) throw new ArgumentNullException("entity");
			if (xElement == null) throw new ArgumentNullException("xElement");

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
			if (string.IsNullOrEmpty(turnos)) throw new ArgumentNullException("turnos");
			IEnumerable<string> turnosCollection = turnos.Substring(2, turnos.Length - 2).Split(new char[] { ';', '#' });
			return turnosCollection;
		}
	}
}
