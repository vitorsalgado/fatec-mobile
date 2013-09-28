using System.Web.Mvc;

namespace Fatec.Core
{
	public class EngineWrapper
	{
		private static IEngine _instance;
		private static object _locker = new object();
		private EngineWrapper() { }

		public static IEngine Current
		{
			get { lock (_locker) { return _instance ?? (_instance = DependencyResolver.Current.GetService<IEngine>()); } }
		}
	}
}
