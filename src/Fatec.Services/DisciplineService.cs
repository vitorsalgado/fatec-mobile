using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class DisciplineService : IDisciplineService
	{
		private readonly IClassAssignmentRepository _classAssignmentRepository;
		private readonly ICacheManager _cacheStrategy;

		private const string CACHE_DISCIPLINE_BY_ID = "fatec.domain.disciplina-{0}";
		private const int CACHE_EXPIRATION_TIME = 2440;

		public DisciplineService(
			IClassAssignmentRepository classAssignmentRepository, ICacheManager cacheManager)
		{
			_classAssignmentRepository = classAssignmentRepository;
			_cacheStrategy = cacheManager;
		}

		public Discipline GetById(int id)
		{
			var cacheKey = string.Format(CACHE_DISCIPLINE_BY_ID, id);

			return _cacheStrategy.Get(cacheKey, CACHE_EXPIRATION_TIME, () =>
			{
				return _classAssignmentRepository.GetDisciplineById(id);
			});
		}

		public ICollection<Discipline> GetAllDisciplines()
		{
			return _classAssignmentRepository.GetAllDisciplines();
		}
	}
}
