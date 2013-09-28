using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;

namespace Fatec.Core.Domain
{
	public class Discipline : AuditedEntity
	{
		private readonly IClassAssignmentRepository _classAssignmentRepository;
		private readonly ICacheStrategy _cacheStrategy;

		private const string CACHE_DISCIPLINE_BY_ID = "fatec.discipline.id-{0}";
		private const int CACHE_EXPIRATION_TIME = 2440;

		public Discipline(int id)
		{
			_classAssignmentRepository = EngineWrapper.Current.Resolve<IClassAssignmentRepository>();
			_cacheStrategy = EngineWrapper.Current.Resolve<ICacheStrategy>();

			var cacheKey = string.Format(CACHE_DISCIPLINE_BY_ID, id);

			var discipline = _cacheStrategy.Get(cacheKey, CACHE_EXPIRATION_TIME, () =>
			{
				return _classAssignmentRepository.GetDisciplineById(id);
			});

			Acronym = discipline.Acronym;
			Name = discipline.Name;
			Cycle = discipline.Cycle;
		}

		public Discipline() { }

		public string Acronym { get; set; }
		public string Name { get; set; }
		public string Cycle { get; set; }

	}
}
