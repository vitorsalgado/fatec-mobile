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

			routes.LowercaseUrls = true;

			routes.MapRoute(
				name: "noticias-detalhe",
				url: "{controller}/noticias/{id}/{titulo}/{assunto}",
				defaults: new { controller = "Fatec", action = "Noticia", assunto = UrlParameter.Optional }
			);

			routes.MapRoute(
				name: "cursos-detalhe",
				url: "fatec/cursos/{id}/{nome}",
				defaults: new { controller = "Fatec", action = "Curso" }
			);

			routes.MapRoute(
				name: "Login",
				url: "Entrar/",
				defaults: new { controller = "Account", action = "Login" }
			);

			routes.MapRoute(
				name: "Sobre",
				url: "sobre/",
				defaults: new { controller = "Home", action = "Sobre" }
			);

			routes.MapRoute(
				name: "Feedback",
				url: "feedback/",
				defaults: new { controller = "Home", action = "Feedback" }
			);

			routes.MapRoute(
				name: "404",
				url: "404/",
				defaults: new { controller = "Error", action = "NotFound" }
			);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}