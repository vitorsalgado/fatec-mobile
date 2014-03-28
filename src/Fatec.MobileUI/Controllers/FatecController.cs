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
	public class FatecController : Controller
	{
		private readonly INewsService _newsService;
		private readonly IFatecService _fatecService;

		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public FatecController(INewsService newsService, IFatecService fatecService)
		{
			_newsService = newsService;
			_fatecService = fatecService;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Cursos()
		{
			var model = new List<CourseModel>();
			var cursos = await Task.Run(() => _fatecService.GetActivesCourses());

			foreach (var curso in cursos)
				model.Add(curso.ToModel());

			return View(model);
		}

		[AllowAnonymous]
		[BackButtonAction("Cursos", "Fatec")]
		public async Task<ActionResult> Curso(int id, string nome)
		{
			var model = new CourseModel();
			var curso = await Task.Run(() => _fatecService.GetCourseById(id));

			if (nome != WebHelper.ToSeoFriendly(curso.Name))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = WebHelper.ToSeoFriendly(curso.Name) });

			return View(curso.ToModel());
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Fatec")]
		[SetPageInfoLabels("Notícias", "notícias", " da fatec praia grande")]
		public async Task<ActionResult> Noticias(string q)
		{
			var model = new List<NewsModel>();
			var avisos = await Task.Run(() => _newsService.GetAllFatecNews());

			if (!string.IsNullOrEmpty(q))
			{
				avisos = avisos
					.Where(x => x.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Noticias";
			}

			if (avisos.Count == 0)
			{
				ModelState.AddModelError("", "Não foram encontrados avisos da FATEC.");
				return View(model);
			}

			avisos.ToList().ForEach(x => model.Add(x.ToModel()));

			return View(model);
		}

		[SetSearchFormAction("Noticias")]
		[BackButtonAction("Noticias", "Fatec")]
		[SetPageInfoLabels("Notícias", "notícia", " da fatec praia grande")]
		public async Task<ActionResult> Noticia(int id, string titulo)
		{
			var model = new NewsModel();
			var aviso = await Task.Run(() => _newsService.GetFatecNews(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(aviso.Title);

			if (!titulo.Equals(seoFriendlyUrl, StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl });

			model = aviso.ToModel();

			return View(model);
		}

		[AllowAnonymous]
		public ActionResult Sobre()
		{
			return View();
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Faltas(string q)
		{
			var model = new List<TeacherAbsenceModel>();
			var faltas = await Task.Run(() => _fatecService.GetTeachersAbsences());

			if (!string.IsNullOrEmpty(q))
			{
				faltas = faltas
					.Where(x => x.TeacherName != null && x.TeacherName.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Faltas";
			}

			if (faltas.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma falta de professor encontrada.");
				return View(model);
			}

			foreach (var falta in faltas)
				model.Add(falta.ToModel());

			return View(model);
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Reposicoes(string q)
		{
			var model = new List<ClassReplacementModel>();
			var replacements = await Task.Run(() => _fatecService.GetClassReplacements());

			if (!string.IsNullOrEmpty(q))
			{
				replacements = replacements
					.Where(x => x.TeacherName != null && x.TeacherName.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Reposicoes";
			}

			if (replacements.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma reposição encontrada.");
				return View(model);
			}

			foreach (var replacement in replacements)
				model.Add(replacement.ToModel());

			return View(model);
		}
	}
}
