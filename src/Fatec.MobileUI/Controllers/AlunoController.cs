﻿using Fatec.Core;
using Fatec.Core.Domain;
using Fatec.MobileUI.Filters;
using Fatec.MobileUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fatec.MobileUI.Controllers
{
    public class AlunoController : Controller
	{
		private readonly IWorkContext _workContext;
		private const string BACK_BUTTON_ACTION_NAME = "BackButtonActionName";

		public AlunoController(IWorkContext workContext)
		{
			_workContext = workContext;
		}

		public ActionResult Index()
		{
			return View();
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Aluno")]
        public ActionResult Matriculas(string q)
        {
			string enrollment = _workContext.CurrentUsername;
			var model = new List<MatriculaModel>();
			var matriculas = new Student(enrollment).EnrolledDisciplines;

			if (!string.IsNullOrEmpty(q))
			{
				matriculas = matriculas
					.Where(x => x.Discipline.Name != null && x.Discipline.Name.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Matriculas";
			}

			if (matriculas.Count == 0)
			{
				ModelState.AddModelError("", "Nenhuma matrícula encontrada!");
				return View(model);
			}

			matriculas.ToList().ForEach(x => model.Add(x.ToModel()));

            return View(model);
        }

		[BackButtonAction("Index", "Aluno")]
		public ActionResult Aproveitamentos()
		{
			string enrollment = _workContext.CurrentUsername;
			var model = new List<StudiesAdvanceModel>();
			var aproveitamentos = new Student(enrollment).StudiesAdvances;

			if (aproveitamentos.Count == 0)
			{
				ModelState.AddModelError("", "Você não possui nenhum aproveitamento de estudo.");
				return View(model);
			}

			foreach (var aproveitamento in aproveitamentos)
				model.Add(new StudiesAdvanceModel()
				{
					Disciplina = aproveitamento.Discipline.Acronym,
					ParecerDoProfessor = aproveitamento.TeacherDecision,
					Semestre = aproveitamento.Semester,
					Situacao = aproveitamento.Situation,
					Turno = aproveitamento.Period
				});

			return View(model);
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Aluno")]
		public ActionResult Avaliacoes(string q)
		{
			var model = new List<ExamModel>();
			var exams = new Student(_workContext.CurrentUsername).Exams;

			if (!string.IsNullOrEmpty(q))
			{
				exams = exams
					.Where(x => x.Discipline.Name != null && x.Discipline.Name.IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Avaliacoes";
			}

			if (exams.Count == 0)
			{
				ModelState.AddModelError("", "Nehuma avaliação encotrada!");
				return View(model);
			}

			foreach (var exam in exams)
				model.Add(new ExamModel()
				{
					DisciplineName = exam.Discipline.Name,
					FirstExamDate = exam.FirstExamDate,
					Period = exam.Period,
					SecondExamDate = exam.SecondExamDate,
					TeacherName = exam.Professor
				});

			return View(model);
		}

		[SetSearchFormAction]
		[BackButtonAction("Index", "Aluno")]
		public ActionResult Requerimentos(string q)
		{
			var model = new List<RequirementModel>();
			var requirements = new Student(_workContext.CurrentUsername).Requirements;

			if (!string.IsNullOrEmpty(q))
			{
				requirements = requirements
					.Where(x => x.Id.ToString().IndexOf(q, StringComparison.InvariantCultureIgnoreCase) >= 0)
					.ToList();

				ViewData[BACK_BUTTON_ACTION_NAME] = "Requerimentos";
			}

			if (requirements.Count == 0)
			{
				ModelState.AddModelError("", "Nenhum requerimento encontrado.");
				return View(model);
			}

			foreach (var requirement in requirements)
				model.Add(new RequirementModel()
				{
					Id = requirement.Id,
					Category = requirement.Category,
					Comments = requirement.Comments,
					Description = requirement.Description,
					EndDate = requirement.EndDate,
					Result = requirement.Result
				});

			return View(model);
		}
    }
}
