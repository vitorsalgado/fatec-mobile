using Fatec.Core;
using Fatec.Core.Services;
using Fatec.MobileUI.Filters;
using Fatec.MobileUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public ActionResult Noticias(string q)
		{
			var model = new List<AnnouncementsModel>();
			var avisos = _avisosService.GetValidIntershipOpportunities();

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
		public ActionResult Noticia(int id, string titulo)
		{
			if (id <= 0)
				RedirectToAction("Noticias");

			var model = new AnnouncementsModel();
			var aviso = _avisosService.GetInternshipOpportunityById(id);

			if (titulo != CommonHelper.ToSeoFriendly(aviso.Title))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = CommonHelper.ToSeoFriendly(aviso.Title) });

			model = aviso.ToModel();

			return View(model);
		}
    }
}
