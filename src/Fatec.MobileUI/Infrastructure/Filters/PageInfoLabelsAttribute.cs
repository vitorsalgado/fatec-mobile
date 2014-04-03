using System;
using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class PageInfoLabelsAttribute : ActionFilterAttribute
	{
		public string Title { get; private set; }
		public string HighLight { get; private set; }
		public string AdditionalPageInfo { get; private set; }

		public PageInfoLabelsAttribute(string title, string highLight, string additionalPageInfo)
		{
			Title = title;
			HighLight = highLight;
			AdditionalPageInfo = additionalPageInfo;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null) throw new ArgumentNullException("filterContext");

			filterContext.Controller.ViewBag.Title = Title;
			filterContext.Controller.ViewBag.Highlight = HighLight;
			filterContext.Controller.ViewBag.AdditionalPageInfo = AdditionalPageInfo;
		}
	}
}