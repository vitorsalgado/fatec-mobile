using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class DisciplineService : IDisciplineService
	{
		private readonly IDisciplineRepository _disciplineRepository;
		private readonly ICacheManager _cacheManager;

		private const string CACHE_DISCIPLINE_BY_ID = "fatec.domain.discipline-{0}";
		private const int CACHE_EXPIRATION_TIME = 2440;

		public DisciplineService(
			IDisciplineRepository disciplineRepository, ICacheManager cacheManager)
		{
			_disciplineRepository = disciplineRepository;
			_cacheManager = cacheManager;
		}

		public Discipline GetById(int id)
		{
			var cacheKey = string.Format(CACHE_DISCIPLINE_BY_ID, id);

			return _cacheManager.Get(cacheKey, CACHE_EXPIRATION_TIME, () =>
			{
				return _disciplineRepository.GetById(id);
			});
		}

		public ICollection<Discipline> GetAllDisciplines()
		{
			return _disciplineRepository.GetAll();
		}
	}
}
