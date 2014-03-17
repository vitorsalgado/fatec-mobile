using Fatec.Core;
using Fatec.Core.Services;
using Fatec.MobileUI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

		public async Task<ActionResult> Noticias()
		{
			var model = new List<AnnouncementsModel>();
			var avisos = await Task.Run(() =>_avisosService.GetTIValidAnnouncements());
			avisos.ToList().ForEach(x => model.Add(x.ToModel()));

			if (avisos.Count == 0)
				ModelState.AddModelError("", "Não foram encontrados avisos do Departamento de Informática.");

			ViewBag.Title = "Avisos";
			ViewBag.Highlight = "avisos";
			ViewBag.AdditionalPageInfo = "do departamento de tecnologia";

			return View(model);
		}

		public async Task<ActionResult> Noticia(int id, string titulo)
		{
			if (id <= 0)
				RedirectToAction("Noticias");

			var model = new AnnouncementsModel();
			var aviso = await Task.Run(() => _avisosService.GetTIAnnouncementById(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(aviso.Title);

			if (!titulo.Equals(seoFriendlyUrl, System.StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl });

			model = aviso.ToModel();

			ViewBag.Title = "Notícias";
			ViewBag.Highlight = "notícia";
			ViewBag.AdditionalPageInfo = " do C.I.";

			return View(model);
		}
    }
}
