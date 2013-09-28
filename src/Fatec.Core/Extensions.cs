namespace Fatec.Core
{
	public static class Extensions
	{
		public static string Shrink(this string value, int maxSize)
		{
			if (value.Length > maxSize)
				return string.Concat(value.Substring(0, 10), "...");
			return value;
		}
	}
}
