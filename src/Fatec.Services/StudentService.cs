using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.Services
{
	public class StudentService : IStudentService
	{
		private const string CACHE_ALUNO_BY_MATRICULA = "fatec.aluno.matricula-{0}";
		private const string CACHE_ALUNO_EXAMES = "fatec.aluno.exames-{0}";

		private readonly IStudentRepository _studentRepository;
		private readonly ICacheManager _cacheStrategy;
		private readonly IFatecService _fatecService;

		public Student GetByEnrollment(string enrollment)
		{
			throw new NotImplementedException();
		}

		public ICollection<EnrolledDiscipline> GetEnrolledDisciplinesByEnrollment(string enrollment)
		{
			throw new NotImplementedException();
		}

		public ICollection<StudiesAdvance> GetStudiesAdvanceByEnrollment(string enrollment)
		{
			throw new NotImplementedException();
		}

		public ICollection<Exam> GetExamsByEnrollment(string enrollment)
		{
			throw new NotImplementedException();
		}

		public ICollection<Requirement> GetRequirements(string enrollment)
		{
			throw new NotImplementedException();
		}
	}
}
