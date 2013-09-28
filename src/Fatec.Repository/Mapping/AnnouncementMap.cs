using Fatec.Core.Domain;
using System;
using System.Xml.Linq;

namespace Fatec.Repository.Mapping
{
	internal class AnnouncementMap : BaseMapper
	{
		internal static Func<XElement, Announcement> Map = xElement =>
		{
			var aviso = new Announcement();
			aviso.Title = xElement.GetAttrValue<string>("ows_Title");
			aviso.Body = xElement.GetAttrValue<string>("ows_Body");
			FillDefaultFields(aviso, xElement);
			return aviso;
		};
	}
}
