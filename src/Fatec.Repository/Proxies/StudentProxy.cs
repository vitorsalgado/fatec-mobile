using Fatec.Core;
using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using System.Collections.Generic;

namespace Fatec.Repository.Proxies
{
	public class StudentProxy : Student
	{
		private IStudentRepository _alunoRepository = EngineWrapper.Current.Resolve<IStudentRepository>();
		private ICourseRepository _cursosRepository = EngineWrapper.Current.Resolve<ICourseRepository>();

		public StudentProxy(string enrollment) : base(enrollment) { }

		protected Course _course;
		public override Course Course
		{
			get { return _course ?? (_course = _cursosRepository.GetById(CourseId)); }
			set { base.Course = value; }
		}

		public override ICollection<EnrolledDiscipline> EnrolledDisciplines
		{
			get { return _alunoRepository.GetEnrolledDisciplinesByEnrollment(base.Enrollment); }
			protected set { base.EnrolledDisciplines = value; }
		}

		public override ICollection<StudiesAdvance> StudiesAdvances
		{
			get { return _alunoRepository.GetStudiesAdvanceByEnrollment(base.Enrollment); }
			protected set { base.StudiesAdvances = value; }
		}
	}
}
