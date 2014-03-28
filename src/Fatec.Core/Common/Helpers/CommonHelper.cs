using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Fatec.Core
{
	public static class CommonHelper
	{
		public static string ShrinkText(string value, int maxAllowedSize, int toNewSize)
		{
			return ShrinkText(value, maxAllowedSize, toNewSize, "..");
		}

		public static string ShrinkText(string value, int maxAllowedSize, int toNewSize, string textEndingPattern)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;

			if (value.Length > maxAllowedSize)
				return string.Concat(value.Substring(0, toNewSize), textEndingPattern);

			return value;
		}

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
