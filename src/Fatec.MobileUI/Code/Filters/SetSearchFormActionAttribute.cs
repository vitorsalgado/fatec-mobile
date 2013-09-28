using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	public class SetSearchFormActionAttribute : ActionFilterAttribute
	{
		private string _actionName;

		public SetSearchFormActionAttribute() { }

		public SetSearchFormActionAttribute(string actionName)
		{
			_actionName = actionName;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (string.IsNullOrEmpty(_actionName))
				_actionName = filterContext.ActionDescriptor.ActionName;
			filterContext.Controller.ViewData["SearchFormActionName"] = _actionName;
		}

		public override void OnResultExecuted(ResultExecutedContext filterContext)
		{
			filterContext.HttpContext.Response.Write("<script type='text/javascript'>allowSearch = true;</script>");
		}
	}
}