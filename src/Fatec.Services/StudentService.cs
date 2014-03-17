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
		private const string CACHE_STUDENT_BY_ENROLLMENT = "fatec.domain.aluno.matricula-{0}";
		private const string CACHE_STUDENT_EXAMS = "fatec.domain.aluno.avaliacoes-{0}";
		private const string CACHE_STUDENT_DISCIPLINES = "fatec.domain.aluno.disciplinas-{0}";

		private const int CACHE_DURATION = 60000;
		private const int CACHE_MIN_DURATION = 60;

		private readonly IStudentRepository _studentRepository;
		private readonly ICacheManager _cacheManager;
		private readonly IFatecService _fatecService;
		private readonly IDisciplineService _disciplineService;

		public StudentService(
			IStudentRepository studentRepository, ICacheManager cacheManager, IFatecService fatecService, IDisciplineService disciplineService)
		{
			_studentRepository = studentRepository;
			_cacheManager = cacheManager;
			_fatecService = fatecService;
			_disciplineService = disciplineService;
		}

		public Student GetByEnrollment(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var key = string.Format(CACHE_STUDENT_BY_ENROLLMENT, enrollment);

			return _cacheManager.Get(key, CACHE_DURATION, () =>
			{
				return _studentRepository.GetByEnrollment(enrollment);
			});
		}

		public ICollection<EnrolledDiscipline> GetEnrolledDisciplinesByEnrollment(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");

			var key = string.Format(CACHE_STUDENT_BY_ENROLLMENT, enrollment);

			var enrolledDisciplines = _cacheManager.Get(CACHE_STUDENT_DISCIPLINES, CACHE_MIN_DURATION, () =>
			{
				return _studentRepository.GetEnrolledDisciplinesByEnrollment(enrollment);
			});

			foreach (var discipline in enrolledDisciplines)
				discipline.Discipline = _disciplineService.GetById(discipline.DisciplineId);

			return enrolledDisciplines;
		}

		public ICollection<StudiesAdvance> GetStudiesAdvanceByEnrollment(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");
			return _studentRepository.GetStudiesAdvanceByEnrollment(enrollment);
		}

		public ICollection<Exam> GetExamsByEnrollment(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");
			
			var exams = _studentRepository.GetExamsByEnrollment(enrollment);
			foreach (var exam in exams)
				exam.Discipline = _disciplineService.GetById(exam.DisciplineId);

			return exams;
		}

		public ICollection<Requirement> GetRequirements(string enrollment)
		{
			if (string.IsNullOrEmpty(enrollment)) throw new ArgumentNullException("enrollment");
			return _studentRepository.GetRequirementsByEnrollment(enrollment);
		}
	}
}
