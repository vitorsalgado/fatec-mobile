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
	public class EstagioController : Controller
	{
		private readonly IAnnouncementService _avisosService;
		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public EstagioController(IAnnouncementService avisoService)
		{
			_avisosService = avisoService;
		}

		[SetSearchFormAction]
		[SetPageInfoLabels("Oportunidades", "oportunidades", "de estágio")]
		public async Task<ActionResult> Noticias(string q)
		{
			var model = new List<AnnouncementsModel>();
			var avisos = await Task.Run(() => _avisosService.GetValidIntershipOpportunities());

			if (!string.IsNullOrEmpty(q))
			{
				avisos = avisos
					.Where(x => x.Title != null && x.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Noticias";
			}

			if (avisos.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma oportunidade de estágio encontrada.");
				return View(model);
			}

			avisos.ToList().ForEach(x => model.Add(x.ToModel()));

			return View(model);
		}

		[SetSearchFormAction("Noticias")]
		[SetPageInfoLabels("Oportunidades", "oportunidade", "de estágio")]
		public async Task<ActionResult> Noticia(int id, string titulo)
		{
			if (id <= 0)
				RedirectToAction("Noticias");

			var model = new AnnouncementsModel();
			var aviso = await Task.Run(() => _avisosService.GetInternshipOpportunityById(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(aviso.Title);

			if (!titulo.Equals(seoFriendlyUrl, StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl });

			model = aviso.ToModel();

			return View(model);
		}
	}
}
