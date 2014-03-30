using System.Web.Optimization;

namespace Fatec.MobileUI
{
	public static class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/core").Include(
				"~/Scripts/jquery-2.1.0.min.js",
				"~/Scripts/core.js"
			));

			bundles.Add(new StyleBundle("~/Content/css")
				.Include("~/Content/font-awesome.min.css")
				.Include("~/Content/base.css")
				.Include("~/Content/tile.css")
				.Include("~/Content/content.css")
				.Include("~/Content/menu.css")
				.Include("~/Content/misc.css")
			);
		}
	}
}