using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;

namespace Fatec.Services
{
	public class DisciplineService : IDisciplineService
	{
		private readonly IClassAssignmentRepository _classAssignmentRepository;
		private readonly ICacheManager _cacheStrategy;

		private const string CACHE_DISCIPLINE_BY_ID = "fatec.discipline.id-{0}";
		private const int CACHE_EXPIRATION_TIME = 2440;

		public Discipline GetById(int id)
		{
			var cacheKey = string.Format(CACHE_DISCIPLINE_BY_ID, id);

			return _cacheStrategy.Get(cacheKey, CACHE_EXPIRATION_TIME, () =>
			{
				return _classAssignmentRepository.GetDisciplineById(id);
			});
		}
	}
}
