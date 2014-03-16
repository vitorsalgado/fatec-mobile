using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Linq;

namespace Fatec.Repositories
{
	public static class SPExtensions
	{
		public static T GetAttrValue<T>(this XElement xElement, string attrName)
		{
			if (string.IsNullOrEmpty(attrName)) throw new ArgumentNullException("attrName");

			if (xElement.Attribute(attrName) == null)
				return default(T);

			var value = xElement.Attribute(attrName).Value;
			if (!String.IsNullOrEmpty(value))
				return TryParse<T>(value);
			else 
				return default(T);
		}

		public static string RemoveDomain(this string value)
		{
			if (string.IsNullOrEmpty(value)) 
				return string.Empty;
			return value.Replace(@"FATECPG\", "").Replace(@"fatecpg\", "");
		}

		private static T TryParse<T>(string value)
		{
			try
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
				return (T)converter.ConvertFromString(null, CultureInfo.InvariantCulture, value);
			}
			catch
			{
				return default(T);
			}
		}
	}
}
