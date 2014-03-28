using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repositories.Mapping
{
	public class NewsMap : BaseMapper
	{
		public static Func<XElement, News> Map = xElement =>
		{
			var aviso = new News();
			aviso.Title = xElement.GetAttrValue<string>("ows_Title");
			aviso.Body = xElement.GetAttrValue<string>("ows_Body");
			FillDefaultFields(aviso, xElement);
			return aviso;
		};
	}
}
