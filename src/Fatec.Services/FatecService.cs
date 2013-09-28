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
		private readonly ICacheStrategy _cacheStrategy;

		private const string CACHE_CURSO_BY_ID = "fatec.curso.id-{0}";
		private const string CACHE_CURSOS = "fatec.cursos.all";

		private const int CACHE_DURATION = int.MaxValue;

		public FatecService(ICourseRepository cursoRepository, IFatecRepository fatecRepository, ICacheStrategy cacheStrategy)
		{
			_cursoRepository = cursoRepository;
			_fatecRepository = fatecRepository;
			_cacheStrategy = cacheStrategy;
		}

		public ICollection<Course> GetActivesCourses()
		{
			return _cacheStrategy.Get(CACHE_CURSOS, CACHE_DURATION, () =>
			{
				return _cursoRepository.GetAllActive();
			});
		}

		public Course GetCourseById(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "Parameter \"id\" must be greather or equal than zero.");

			var cacheKey = string.Format(CACHE_CURSO_BY_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_DURATION, () =>
			{
				var curso = _cursoRepository.GetById(id);
				return curso;
			});
		}

		public ICollection<TeacherAbsence> GetTeachersAbsences()
		{
			return _fatecRepository.GetTeacherAbsences();
		}

		public ICollection<ClassReplacement> GetClassReplacements()
		{
			return _fatecRepository.GetVigentClassReplacements();
		}
	}
}
