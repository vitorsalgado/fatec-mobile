using System.Web.Mvc;
using System.Web.Routing;

namespace Fatec.MobileUI
{
	public static class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("favicon.ico");
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				name: "noticias-detalhe",
				url: "{controller}/noticias/{id}/{titulo}/{assunto}",
				defaults: new { controller = "Fatec", action = "Noticia" }
			);

			routes.MapRoute(
				name: "cursos-detalhe",
				url: "fatec/cursos/{id}/{nome}",
				defaults: new { controller = "Fatec", action = "Curso" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}