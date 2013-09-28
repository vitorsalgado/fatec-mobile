﻿using System;
using System.Text;

namespace Fatec.Core
{
	public static class CommonHelper
	{
		public static bool IsAbsolute(string url)
		{
			Uri result = null;
			return Uri.TryCreate(url, UriKind.Absolute, out result);
		}

		public static string ToSeoFriendly(string value)
		{
			if (value == null) return "";

			const int maxlen = 80;
			int len = value.Length;
			bool prevdash = false;
			var sb = new StringBuilder(len);
			char c;

			for (int i = 0; i < len; i++)
			{
				c = value[i];
				if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
				{
					sb.Append(c);
					prevdash = false;
				}
				else if (c >= 'A' && c <= 'Z')
				{
					sb.Append((char)(c | 32));
					prevdash = false;
				}
				else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
					c == '\\' || c == '-' || c == '_' || c == '=')
				{
					if (!prevdash && sb.Length > 0)
					{
						sb.Append('-');
						prevdash = true;
					}
				}
				else if ((int)c >= 128)
				{
					int prevlen = sb.Length;
					sb.Append(CommonHelper.RemapInternationalCharToAscii(c));
					if (prevlen != sb.Length) prevdash = false;
				}
				if (i == maxlen) break;
			}

			if (prevdash)
				return sb.ToString().Substring(0, sb.Length - 1);
			else
				return sb.ToString();
		}

		public static string RemapInternationalCharToAscii(char c)
		{
			string s = c.ToString().ToLowerInvariant();
			if ("àåáâäãåą".Contains(s))
				return "a";
			else if ("èéêëę".Contains(s))
				return "e";
			else if ("ìíîïı".Contains(s))
				return "i";
			else if ("òóôõöøőð".Contains(s))
				return "o";
			else if ("ùúûüŭů".Contains(s))
				return "u";
			else if ("çćčĉ".Contains(s))
				return "c";
			else if ("żźž".Contains(s))
				return "z";
			else if ("śşšŝ".Contains(s))
				return "s";
			else if ("ñń".Contains(s))
				return "n";
			else if ("ýÿ".Contains(s))
				return "y";
			else if ("ğĝ".Contains(s))
				return "g";
			else if (c == 'ř')
				return "r";
			else if (c == 'ł')
				return "l";
			else if (c == 'đ')
				return "d";
			else if (c == 'ß')
				return "ss";
			else if (c == 'Þ')
				return "th";
			else if (c == 'ĥ')
				return "h";
			else if (c == 'ĵ')
				return "j";
			else
				return "";
		}
	}
}
