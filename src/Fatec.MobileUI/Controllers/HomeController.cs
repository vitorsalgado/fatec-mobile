using Fatec.Core;
using Fatec.Core.Services;
using Fatec.MobileUI.Filters;
using Fatec.MobileUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
	public class HomeController : Controller
	{
		private readonly INewsService _newsService;
		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public HomeController(INewsService avisoService)
		{
			_newsService = avisoService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Social()
		{
			return View();
		}

		[SetSearchFormAction]
		[SetPageInfoLabels("Notícias", "notícias", "da fatec e etec pg")]
		public async Task<ActionResult> Noticias(string q)
		{
			var model = new List<NewsModel>();
			var avisos = await Task.Run(() => _newsService.GetAllHomeNews());

			if (!string.IsNullOrEmpty(q))
			{
				avisos = avisos
					.Where(x => x.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Noticias";
			}

			if (avisos.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma notícia encontrada.");
				return View(model);
			}

			avisos.ToList().ForEach(x => model.Add(x.ToModel()));

			return View(model);
		}

		[SetSearchFormAction("Noticias")]
		[BackButtonAction("Noticias", "Home")]
		[SetPageInfoLabels("Notícias", "notícia", "da fatec e etec pg")]
		public async Task<ActionResult> Noticia(int id, string titulo)
		{
			var model = new NewsModel();
			var aviso = await Task.Run(() => _newsService.GetHomeNews(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(aviso.Title);

			if (!titulo.Equals(seoFriendlyUrl, StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl });

			model = aviso.ToModel();

			return View(model);
		}
	}
}
