using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Fatec.Test
{
	public static class TestEngine
	{
		public static void Start()
		{
			HttpContext.Current = new HttpContext(
				new HttpRequest("none", "http://m.fatecpg.com.br", ""),
				new HttpResponse(new StreamWriter(new MemoryStream()))
			);

			//var resolver = IoC.GetResolver();
			//DependencyResolver.SetResolver(resolver);
		}

		public static T Resolve<T>()
		{
			return DependencyResolver.Current.GetService<T>();
		}
	}
}
