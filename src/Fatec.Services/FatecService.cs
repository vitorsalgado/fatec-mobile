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
		private readonly ICourseRepository _courseRepository;
		private readonly IFatecRepository _fatecRepository;
		private readonly ICacheManager _cacheManager;

		private readonly IDisciplineService _disciplineService;

		private const string CACHE_COURSE_BY_ID = "fatec.core.domain.course-{0}";
		private const string COURSES = "fatec.core.domain.courses";
		private const string CACHE_TEACHERS = "fatec.core.domain.teachers";
		private const string CACHE_REPLACEMENTS = "fatec.core.domain.replacements";

		private const int CACHE_LONG_DURATION = int.MaxValue;
		private const int CACHE_MIN_DURATION = 10;

		public FatecService(
			ICourseRepository cursoRepository,
			IFatecRepository fatecRepository,
			ICacheManager cacheStrategy,
			IDisciplineService disciplineService)
		{
			_courseRepository = cursoRepository;
			_fatecRepository = fatecRepository;
			_cacheManager = cacheStrategy;
			_disciplineService = disciplineService;
		}

		public ICollection<Course> GetActivesCourses()
		{
			return _cacheManager.Get(COURSES, CACHE_LONG_DURATION, () =>
			{
				return _courseRepository.GetAllActive();
			});
		}

		public Course GetCourseById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "Parameter \"id\" must be greather or equal than zero.");

			var cacheKey = string.Format(CACHE_COURSE_BY_ID, id);

			return _cacheManager.Get(cacheKey, CACHE_LONG_DURATION, () =>
			{
				return _courseRepository.GetById(id);
			});
		}

		public ICollection<TeacherAbsence> GetTeachersAbsences()
		{
			ICollection<TeacherAbsence> abscenses = _cacheManager.Get<ICollection<TeacherAbsence>>(CACHE_TEACHERS);
			if (abscenses != null)
				return abscenses;

			abscenses = _fatecRepository.GetTeacherAbsences();

			foreach (var abscense in abscenses)
				abscense.Discipline = _disciplineService.GetById(abscense.DisciplineId);

			_cacheManager.Add(CACHE_TEACHERS, abscenses, CACHE_MIN_DURATION);

			return abscenses;
		}

		public ICollection<Replacement> GetClassReplacements()
		{
			ICollection<Replacement> replacements = _cacheManager.Get<ICollection<Replacement>>(CACHE_REPLACEMENTS);
			if (replacements != null)
				return replacements;

			replacements = _fatecRepository.GetReplacements();

			foreach (var replacement in replacements)
				replacement.Discipline = _disciplineService.GetById(replacement.DisciplineId);

			_cacheManager.Add(CACHE_REPLACEMENTS, replacements, 90);

			return replacements;
		}

		public ICollection<KeyMovement> GetKeyMovement()
		{
			return _fatecRepository.GetKeyMovement();
		}
	}
}
