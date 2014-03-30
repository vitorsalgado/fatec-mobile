using System.Web;

namespace Fatec.MobileUI
{
	public static class UrlBuilder
	{
		//Institucional
		public static string ToWWW() { return "http://www.fatecpg.com.br"; }
		public static string ToWebmail() { return "http://mail.google.com/a/fatecpg.com.br"; }
		public static string ToImprensa() { return "http://sites.google.com/a/fatecpg.com.br/imprensa/"; }
		
		//Social
		public static string ToTwitterFatec() { return "http://twitter.com/fatecpg"; }
		public static string ToFacebookFatec() { return "http://pt-br.facebook.com/fatecpg"; }
		public static string ToLinkedinFatec() { return "http://www.linkedin.com/groups?about=&gid=2466055"; }
		public static string ToFlickrFatec() { return "http://www.flickr.com/photos/imprensafatecpg/"; }

		private static string Path(string virtualPath, params object[] args)
		{
			return Path(string.Format(virtualPath, args));
		}

		private static string Path(string virtualPath)
		{
			return VirtualPathUtility.ToAbsolute(virtualPath);
		}
	}
}