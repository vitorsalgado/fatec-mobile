using AutoMapper;
using Fatec.Core;
using Fatec.Core.Domain;
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
	public class EstagioController : Controller
	{
		private readonly INewsService _newsService;
		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public EstagioController(INewsService avisoService)
		{
			_newsService = avisoService;
		}

		[SearchAction]
		[PageInfoLabels("Oportunidades", "oportunidades", "de estágio")]
		public async Task<ActionResult> Noticias(string q)
		{
			var newsCollection = await Task.Run(() => _newsService.GetAllInternships());

			if (!string.IsNullOrEmpty(q))
			{
				newsCollection = newsCollection
					.Where(x => x.Title != null && x.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Noticias";
			}

			IEnumerable<NewsModel> model = new List<NewsModel>();

			if (newsCollection.Count == 0)
				ModelState.AddModelError("", "Nenhuma oportunidade de estágio encontrada :-(");
			else
				model = Mapper.Map<IEnumerable<News>, IEnumerable<NewsModel>>(newsCollection);

			return View(model);
		}

		[SearchAction("Noticias")]
		[PageInfoLabels("Oportunidades", "oportunidade", "de estágio")]
		public async Task<ActionResult> Noticia(int id, string titulo)
		{
			if (id <= 0)
				RedirectToAction("Noticias");

			var model = new NewsModel();
			var news = await Task.Run(() => _newsService.GetInternship(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(news.Title);

			if (!titulo.Equals(seoFriendlyUrl, StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl });

			return View(Mapper.Map<News, NewsModel>(news));
		}
	}
}
