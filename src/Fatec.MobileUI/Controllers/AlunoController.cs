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
	public class AlunoController : Controller
	{
		private readonly IWorkContext _workContext;
		private readonly IStudentService _studentService;

		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public AlunoController(
			IWorkContext workContext, IStudentService studentService)
		{
			_workContext = workContext;
			_studentService = studentService;
		}

		public ActionResult Index()
		{
			return View();
		}

		[SearchAction]
		[BackButtonAction("Index", "Aluno")]
		public async Task<ActionResult> Matriculas(string q)
		{
			string enrollment = _workContext.CurrentUsername;
			var studentEnrolledDisciplines = await Task.Run(() => _studentService.GetEnrolledDisciplines(enrollment));
			IEnumerable<EnrollmentModel> model = new List<EnrollmentModel>();

			if (studentEnrolledDisciplines.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma matrícula encontrada :-(");
				return View(model);
			}

			if (!string.IsNullOrEmpty(q))
			{
				studentEnrolledDisciplines = studentEnrolledDisciplines
					.Where(x => x.Discipline.Name != null && x.Discipline.Name.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Matriculas";
			}

			model = Mapper.Map<IEnumerable<EnrolledDiscipline>, IEnumerable<EnrollmentModel>>(studentEnrolledDisciplines);

			return View(model);
		}

		[BackButtonAction("Index", "Aluno")]
		public async Task<ActionResult> Aproveitamentos()
		{
			string enrollment = _workContext.CurrentUsername;
			var aproveitamentos = await Task.Run(() => _studentService.GetStudiesAdvance(enrollment));

			IEnumerable<AproveitamentoModel> model = new List<AproveitamentoModel>();

			if (aproveitamentos.Count == 0)
				ModelState.AddModelError("", "Você não possui nenhum aproveitamento de estudo :-(");
			else
				model = Mapper.Map<IEnumerable<StudiesAdvance>, IEnumerable<AproveitamentoModel>>(aproveitamentos);

			return View(model);
		}

		[SearchAction]
		[BackButtonAction("Index", "Aluno")]
		public async Task<ActionResult> Avaliacoes(string q)
		{
			IEnumerable<ExamModel> model = new List<ExamModel>();
			var exams = await Task.Run(() => _studentService.GetExams(_workContext.CurrentUsername));

			if (exams.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma avaliação encontrada :-)");
				return View(model);
			}

			if (!string.IsNullOrEmpty(q))
			{
				exams = exams
					.Where(x => x.Discipline.Name != null && x.Discipline.Name.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Avaliacoes";
			}

			model = Mapper.Map<IEnumerable<Exam>, IEnumerable<ExamModel>>(exams);

			return View(model);
		}

		[SearchAction]
		[BackButtonAction("Index", "Aluno")]
		public async Task<ActionResult> Requerimentos(string q)
		{
			var requirements = await Task.Run(() => _studentService.GetRequirements(_workContext.CurrentUsername));

			if (requirements.Count == 0)
			{
				ModelState.AddModelError("", "Nenhum requerimento encontrado :-(");
				return View(new List<RequirementModel>());
			}

			if (!string.IsNullOrEmpty(q))
			{
				requirements = requirements
					.Where(x => x.Id.ToString().IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Requerimentos";
			}

			return View(Mapper.Map<IEnumerable<Requirement>, IEnumerable<RequirementModel>>(requirements));
		}

		[BackButtonAction("Index", "Aluno")]
		public async Task<ActionResult> Historico()
		{
			var model = new HistoryModel();
			var history = await Task.Run(() => _studentService.GetHistory(_workContext.CurrentUsername));

			if (history == null)
			{
				ModelState.AddModelError("", "Não encontramos nenhum registro de histórico para a sua matrícula ;-(");
				return View(model);
			}

			history.Entries = history.Entries.OrderBy(x => x.Discipline.Cycle).ToList();
			model.History = history;

			return View(model);
		}
	}
}
