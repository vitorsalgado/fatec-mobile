using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Fatec.Core
{
	public static class CommonHelper
	{
		public static string SerializeToXml(object obj)
		{
			var xmlS = new XmlSerializer(obj.GetType());
			var sb = new StringBuilder();

			using (var sw = new StringWriter(sb))
			{
				xmlS.Serialize(sw, obj);
				return sb.ToString();
			}
		}
	}
}
