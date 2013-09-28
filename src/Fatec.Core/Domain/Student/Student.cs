using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class Student : AuditedEntity
	{
		private const string CACHE_ALUNO_BY_MATRICULA = "fatec.aluno.matricula-{0}";
		private const string CACHE_ALUNO_EXAMES = "fatec.aluno.exames-{0}";

		private readonly IStudentRepository _studentRepository = EngineWrapper.Current.Resolve<IStudentRepository>();
		private readonly ICacheStrategy _cacheStrategy = EngineWrapper.Current.Resolve<ICacheStrategy>();
		private readonly IFatecService _fatecService = EngineWrapper.Current.Resolve<IFatecService>();

		public Student(string enrollment)
		{
			Enrollment = enrollment;
		}

		public string Enrollment { get; private set; }

		public string Fullname { get; set; }

		public DateTime BirthDate { get; set; }

		public string Period { get; set; }

		public DateTime EnrollDate { get; set; }

		public string SemesterAdmission { get; set; }

		public string RG { get; set; }

		public string HomePhone { get; set; }

		public string CellPhone { get; set; }

		public string Email { get; set; }

		public int CourseId { get; set; }

		private Course _course;
		public virtual Course Course 
		{
			get { return _course ?? (_course = _fatecService.GetCourseById(CourseId)); }
			set { _course = value; } 
		}

		protected ICollection<Exam> _exams;
		public virtual ICollection<Exam> Exams
		{
			get { return _exams ?? (_exams = _studentRepository.GetExamsByEnrollment(Enrollment)); }
			protected set { _exams = value; }
		}

		protected ICollection<EnrolledDiscipline> _enrolledDisciplines;
		public virtual ICollection<EnrolledDiscipline> EnrolledDisciplines
		{
			get { return _enrolledDisciplines ?? (_enrolledDisciplines = _studentRepository.GetEnrolledDisciplinesByEnrollment(Enrollment)); }
			protected set { _enrolledDisciplines = value; }
		}

		protected ICollection<StudiesAdvance> _studiesAdvances;
		public virtual ICollection<StudiesAdvance> StudiesAdvances
		{
			get { return _studiesAdvances ?? (_studiesAdvances = _studentRepository.GetStudiesAdvanceByEnrollment(Enrollment)); }
			protected set { _studiesAdvances = value; }
		}

		protected ICollection<Requirement> _requirements;
		public virtual ICollection<Requirement> Requirements
		{
			get { return _requirements ?? (_requirements = _studentRepository.GetRequirementsByEnrollment(Enrollment)); }
			protected set { _requirements = value; }
		}

		public Student Load()
		{
			return _studentRepository.GetByEnrollment(Enrollment);
		}
	}
}
