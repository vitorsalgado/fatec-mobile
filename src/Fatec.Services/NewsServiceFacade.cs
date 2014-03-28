using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class NewsServiceFacade : INewsService
	{
		private const string CACHE_AVISOS_HOME_ALL = "fatec.avisoshome.all";
		private const string CACHE_AVISO_HOME_ID = "fatec.avisohome.id-{0}";
		private const string CACHE_AVISOS_FATEC_ALL = "fatec.avisosfatec.all";
		private const string CACHE_AVISO_FATEC_ID = "fatec.avisofatec.id-{0}";
		private const string CACHE_ESTAGIO_ID = "fatec.estagio.id-{0}";
		private const string CACHE_ESTAGIO_ALL = "fatec.estagio.all";

		private const int CACHE_MIN_EXPIRATION_TIME = 10;
		private const int CACHE_MAX_EXPIRATION_TIME = 1440;

		private readonly INewsRepository _newsRepository;
		private readonly ICacheManager _cacheStrategy;

		public NewsServiceFacade(
			INewsRepository newsRepository,
			ICacheManager cacheStrategy)
		{
			_newsRepository = newsRepository;
			_cacheStrategy = cacheStrategy;
		}

		#region Home

		public News GetHomeNews(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			var cacheKey = string.Format(CACHE_AVISO_HOME_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetSingleHomeNews(id);
			});
		}

		public ICollection<News> GetAllHomeNews()
		{
			var cacheKey = CACHE_AVISOS_HOME_ALL;
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _newsRepository.GetAllHomeNews();
				return avisosValidos;
			});
		}

		#endregion

		#region Fatec

		public News GetFatecNews(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			var cacheKey = string.Format(CACHE_AVISO_FATEC_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetSingleFatecNews(id);
			});
		}

		public ICollection<News> GetAllFatecNews()
		{
			var cacheKey = CACHE_AVISOS_FATEC_ALL;
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _newsRepository.GetAllFatecNews();
				return avisosValidos;
			});
		}

		#endregion

		#region Estagios

		public News GetInternship(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			string cacheKey = string.Format(CACHE_ESTAGIO_ID, id);
			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetInternship(id);
			});
		}

		public ICollection<News> GetAllInternships()
		{
			return _cacheStrategy.Get(CACHE_ESTAGIO_ALL, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetAllInternships();
			});
		}

		#endregion
	}
}
