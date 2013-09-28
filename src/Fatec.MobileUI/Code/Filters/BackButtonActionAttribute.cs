using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	public class BackButtonActionAttribute : ActionFilterAttribute
	{
		private string _actionName;
		private string _controllerName;

		public BackButtonActionAttribute(string actionName, string controllerName)
		{
			_actionName = actionName;
			_controllerName = controllerName;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			filterContext.Controller.ViewData["BackButtonActionName"] = _actionName;
			filterContext.Controller.ViewData["BackButtonControllerName"] = _controllerName;
		}
	}
}