﻿using Fatec.Core;
using Fatec.Core.Services;
using Fatec.MobileUI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
    public class DTICController : Controller
    {
		private readonly IAnnouncementService _avisosService;

		public DTICController(IAnnouncementService avisoService)
		{
			_avisosService = avisoService;
		}

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Noticias()
		{
			var model = new List<AnnouncementsModel>();
			var avisos = _avisosService.GetTIValidAnnouncements();
			avisos.ToList().ForEach(x => model.Add(x.ToModel()));

			if (avisos.Count == 0)
				ModelState.AddModelError("", "Não foram encontrados avisos do Departamento de Informática.");

			ViewBag.Title = "Avisos";
			ViewBag.Highlight = "avisos";
			ViewBag.AdditionalPageInfo = "do departamento de tecnologia";

			return View(model);
		}

		public ActionResult Noticia(int id, string titulo)
		{
			if (id <= 0)
				RedirectToAction("Noticias");

			var model = new AnnouncementsModel();
			var aviso = _avisosService.GetTIAnnouncementById(id);

			if (titulo != CommonHelper.ToSeoFriendly(aviso.Title))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = CommonHelper.ToSeoFriendly(aviso.Title) });

			model = aviso.ToModel();

			ViewBag.Title = "Notícias";
			ViewBag.Highlight = "notícia";
			ViewBag.AdditionalPageInfo = " do C.I.";

			return View(model);
		}
    }
}