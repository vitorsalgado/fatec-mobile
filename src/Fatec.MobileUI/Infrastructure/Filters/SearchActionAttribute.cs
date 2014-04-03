using System;
using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class SearchActionAttribute : ActionFilterAttribute
	{
		public string ActionName { get; private set; }

		public SearchActionAttribute() { }

		public SearchActionAttribute(string actionName)
		{
			ActionName = actionName;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null) throw new ArgumentNullException("filterContext");

			if (string.IsNullOrEmpty(ActionName))
				ActionName = filterContext.ActionDescriptor.ActionName;

			filterContext.Controller.ViewData["SearchActionName"] = ActionName;
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			if (filterContext == null) throw new ArgumentNullException("filterContext");

			filterContext.HttpContext.Response.Write("<script type='text/javascript'>allowSearch = true;</script>");
		}
	}
}
