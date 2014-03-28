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
	public class FatecController : Controller
	{
		private readonly INewsService _newsService;
		private readonly IFatecService _fatecService;
		private readonly IWorkContext _workContext;

		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public FatecController(
			INewsService newsService, IFatecService fatecService, IWorkContext workContext)
		{
			_newsService = newsService;
			_fatecService = fatecService;
			_workContext = workContext;
		}

		[AllowAnonymous]
		public ActionResult Index()
		{
			return View();
		}

		[AllowAnonymous]
		public ActionResult Sobre()
		{
			return View();
		}

		[AllowAnonymous]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Cursos()
		{
			var courses = await Task.Run(() => _fatecService.GetActivesCourses());
			IEnumerable<CourseModel> model = Mapper.Map<IEnumerable<Course>, IEnumerable<CourseModel>>(courses);

			return View(model);
		}

		[AllowAnonymous]
		[BackButtonAction("Cursos", "Fatec")]
		public async Task<ActionResult> Curso(int id, string nome)
		{
			var model = new CourseModel();
			var course = await Task.Run(() => _fatecService.GetCourseById(id));

			if (nome != WebHelper.ToSeoFriendly(course.Name))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = WebHelper.ToSeoFriendly(course.Name) });

			return View(Mapper.Map<Course, CourseModel>(course));
		}

		[SearchAction]
		[BackButtonAction("Index", "Fatec")]
		[PageInfoLabels("Notícias", "notícias", " da fatec praia grande")]
		public async Task<ActionResult> Noticias(string q)
		{
			IEnumerable<NewsModel> model = new List<NewsModel>();
			var newsCollection = new List<News>();

			newsCollection.AddRange(await Task.Run(() => _newsService.GetAllHomeNews()));

			if (_workContext.CurrentUser.IsAuthenticated)
				newsCollection.AddRange(await Task.Run(() => _newsService.GetAllFatecNews()));

			newsCollection = newsCollection.OrderByDescending(x => x.CreatedOn.Value).ToList();

			if (!string.IsNullOrEmpty(q))
			{
				newsCollection = newsCollection
					.Where(x => x.Title.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Noticias";
			}

			if (newsCollection.Count == 0)
				ModelState.AddModelError("", "Não foram encontrados avisos da FATEC :-(");
			else
				model = Mapper.Map<IEnumerable<News>, IEnumerable<NewsModel>>(newsCollection);

			return View(model);
		}

		[SearchAction("Noticias")]
		[BackButtonAction("Noticias", "Fatec")]
		[PageInfoLabels("Notícias", "notícia", " da fatec praia grande")]
		public async Task<ActionResult> Noticia(int id, string titulo, string assunto)
		{
			var news = new News();

			if ("f".Equals(assunto))
				news = await Task.Run(() => _newsService.GetFatecNews(id));
			else
				news = await Task.Run(() => _newsService.GetHomeNews(id));

			var seoFriendlyUrl = WebHelper.ToSeoFriendly(news.Title);

			if (!titulo.Equals(seoFriendlyUrl, StringComparison.InvariantCultureIgnoreCase))
				return RedirectToActionPermanent("Noticia", new { id = id, titulo = seoFriendlyUrl, assunto = assunto });

			return View(Mapper.Map<News, NewsModel>(news));
		}

		[SearchAction]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Faltas(string q)
		{
			var abscences = await Task.Run(() => _fatecService.GetTeachersAbsences());

			if (!string.IsNullOrEmpty(q))
			{
				abscences = abscences
					.Where(x => x.TeacherName != null && x.TeacherName.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Faltas";
			}

			IEnumerable<TeacherAbsenceModel> model = new List<TeacherAbsenceModel>();

			if (abscences.Count == 0)
				ModelState.AddModelError("", "Nenhuma falta de professor encontrada :-(");
			else
				model = Mapper.Map<IEnumerable<TeacherAbsence>, IEnumerable<TeacherAbsenceModel>>(abscences);

			return View(model);
		}

		[SearchAction]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Reposicoes(string q)
		{
			var replacements = await Task.Run(() => _fatecService.GetClassReplacements());

			if (!string.IsNullOrEmpty(q))
			{
				replacements = replacements
					.Where(x => x.TeacherName != null && x.TeacherName.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Reposicoes";
			}

			IEnumerable<ReplacementModel> model = new List<ReplacementModel>();

			if (replacements.Count == 0)
				ModelState.AddModelError("", "Nenhuma reposição encontrada :-(");
			else
				model = Mapper.Map<IEnumerable<Replacement>, IEnumerable<ReplacementModel>>(replacements);

			return View(model);
		}

		[SearchAction]
		[BackButtonAction("Index", "Fatec")]
		public async Task<ActionResult> Busca(string q)
		{
			var results = await Task.Run(() => _fatecService.GetKeyMovement());
			IEnumerable<KeyMovementModel> model = new List<KeyMovementModel>();

			if (results.Count == 0)
			{
				ModelState.AddModelError("", string.Format("Nenhum professor encontrado para a consulta \"{0}\" :-(", q));
				return View(model);
			}
			else
			{
				if (!string.IsNullOrEmpty(q))
				{
					results = results
						.Where(x => x.Requester != null && x.Requester.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
						.ToList();

					ViewData[BACK_BUTTON_ACTION_NAME] = "Busca";
				}

				model = Mapper.Map<IEnumerable<KeyMovement>, IEnumerable<KeyMovementModel>>(results);
			}

			return View(model);
		}
	}
}
