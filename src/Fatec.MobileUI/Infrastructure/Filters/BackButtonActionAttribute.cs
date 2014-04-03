using System;
using System.Web.Mvc;

namespace Fatec.MobileUI.Filters
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public sealed class BackButtonActionAttribute : ActionFilterAttribute
	{
		public string ActionName { get; private set; }
		public string ControllerName { get; private set; }

		public BackButtonActionAttribute(string actionName, string controllerName)
		{
			if (string.IsNullOrEmpty(actionName)) throw new ArgumentNullException("actionName");
			if (string.IsNullOrEmpty(controllerName)) throw new ArgumentNullException("controllerName");

			ActionName = actionName;
			ControllerName = controllerName;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (filterContext == null) throw new ArgumentNullException("filterContext");

			filterContext.Controller.ViewData["BackButtonActionName"] = ActionName;
			filterContext.Controller.ViewData["BackButtonControllerName"] = ControllerName;
		}
	}
}