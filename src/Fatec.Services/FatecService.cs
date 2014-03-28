using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class FatecService : IFatecService
	{
		private readonly ICourseRepository _cursoRepository;
		private readonly IFatecRepository _fatecRepository;
		private readonly ICacheManager _cacheStrategy;

		private readonly IDisciplineService _disciplineService;

		private const string CACHE_CURSO_BY_ID = "fatec.domain.curso.id-{0}";
		private const string CACHE_CURSOS = "fatec.domain.cursoss";
		private const string CACHE_PROFESSORES = "fatec.domain.professores";
		private const string CACHE_CLASS_REPLACEMENT = "fatec.domain.atribuicoes";

		private const int CACHE_LONG_DURATION = int.MaxValue;
		private const int CACHE_MIN_DURATION = 10;

		public FatecService(
			ICourseRepository cursoRepository,
			IFatecRepository fatecRepository,
			ICacheManager cacheStrategy,
			IDisciplineService disciplineService)
		{
			_cursoRepository = cursoRepository;
			_fatecRepository = fatecRepository;
			_cacheStrategy = cacheStrategy;
			_disciplineService = disciplineService;
		}

		public ICollection<Course> GetActivesCourses()
		{
			return _cacheStrategy.Get(CACHE_CURSOS, CACHE_LONG_DURATION, () =>
			{
				return _cursoRepository.GetAllActive();
			});
		}

		public Course GetCourseById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "Parameter \"id\" must be greather or equal than zero.");

			var cacheKey = string.Format(CACHE_CURSO_BY_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_LONG_DURATION, () =>
			{
				var curso = _cursoRepository.GetById(id);
				return curso;
			});
		}

		public ICollection<TeacherAbsence> GetTeachersAbsences()
		{
			var key = string.Format(CACHE_PROFESSORES);

			var abscenses = _cacheStrategy.Get(key, CACHE_MIN_DURATION, () =>
			{
				return _fatecRepository.GetTeacherAbsences();
			});

			foreach (var abscense in abscenses)
				abscense.Discipline = _disciplineService.GetById(abscense.DisciplineId);

			return abscenses;
		}

		public ICollection<Replacement> GetClassReplacements()
		{
			var classReplacements = _cacheStrategy.Get(
				CACHE_CLASS_REPLACEMENT, 90, () =>
				{
					return _fatecRepository.GetReplacements();
				});

			foreach (var replacement in classReplacements)
				replacement.Discipline = _disciplineService.GetById(replacement.DisciplineId);

			return classReplacements;
		}

		public ICollection<KeyMovement> GetKeyMovement()
		{
			return _fatecRepository.GetKeyMovement();
		}
	}
}
