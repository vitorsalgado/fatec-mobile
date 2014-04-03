using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class StudentService : IStudentService
	{
		private const string CACHE_STUDENT = "fatec.core.domain.aluno-{0}";
		private const string CACHE_STUDENT_EXAMS = "fatec.core.domain.aluno.exams-{0}";
		private const string CACHE_STUDENT_DISCIPLINES = "fatec.core.domain.aluno.disciplines-{0}";
		private const string CACHE_STUDENT_HISTORY = "fatec.core.domain.aluno.history-{0}";

		private const int CACHE_LONG_DURATION = 60000;
		private const int CACHE_MIN_DURATION = 60;
		private const int CACHE_MEDIUM_DURATION = 120;

		private readonly IStudentRepository _studentRepository;
		private readonly ICacheManager _cacheManager;
		private readonly IDisciplineService _disciplineService;

		public StudentService(
			IStudentRepository studentRepository, ICacheManager cacheManager, IDisciplineService disciplineService)
		{
			_studentRepository = studentRepository;
			_cacheManager = cacheManager;
			_disciplineService = disciplineService;
		}

		public Student Get(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var key = string.Format(CACHE_STUDENT, enrollment);
			Func<Student> fetchFunction = () => _studentRepository.Get(enrollment);

			return _cacheManager.Get(key, CACHE_LONG_DURATION, fetchFunction);
		}

		public ICollection<EnrolledDiscipline> GetEnrolledDisciplines(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var key = string.Format(CACHE_STUDENT_DISCIPLINES, enrollment);

			ICollection<EnrolledDiscipline> enrolledDisciplines = _cacheManager.Get<ICollection<EnrolledDiscipline>>(key);
			if (enrolledDisciplines != null)
				return enrolledDisciplines;

			enrolledDisciplines = _studentRepository.GetEnrolledDisciplines(enrollment);

			foreach (var discipline in enrolledDisciplines)
				discipline.Discipline = _disciplineService.GetById(discipline.DisciplineId);

			_cacheManager.Add(key, enrolledDisciplines, CACHE_MIN_DURATION);

			return enrolledDisciplines;
		}

		public ICollection<StudiesAdvance> GetStudiesAdvance(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			return _studentRepository.GetStudiesAdvance(enrollment);
		}

		public ICollection<Exam> GetExams(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var exams = _studentRepository.GetExams(enrollment);
			foreach (var exam in exams)
				exam.Discipline = _disciplineService.GetById(exam.DisciplineId);

			return exams;
		}

		public ICollection<Requirement> GetRequirements(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			return _studentRepository.GetRequirements(enrollment);
		}

		public History GetHistory(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var key = string.Format(CACHE_STUDENT_HISTORY, enrollment);
			Func<History> fetchFunction = () => _studentRepository.GetHistory(enrollment);

			return _cacheManager.Get(key, CACHE_MEDIUM_DURATION, fetchFunction);
		}
	}
}
