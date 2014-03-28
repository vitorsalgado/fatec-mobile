using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	public class PageInfoLabelsAttribute : ActionFilterAttribute
	{
		private string _title;
		private string _highLight;
		private string _additionalPageInfo;

		public PageInfoLabelsAttribute(string title, string highLight, string additionalPageInfo)
		{
			_title = title;
			_highLight = highLight;
			_additionalPageInfo = additionalPageInfo;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.Controller.ViewBag.Title = _title;
			filterContext.Controller.ViewBag.Highlight = _highLight;
			filterContext.Controller.ViewBag.AdditionalPageInfo = _additionalPageInfo;
		}
	}
}