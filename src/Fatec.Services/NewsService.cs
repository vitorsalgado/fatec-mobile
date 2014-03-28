using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;

namespace Fatec.Services
{
	public class NewsService : INewsService
	{
		private const string cacheHomeNewsAll = "fatec.core.domain.home.news";
		private const string cacheHomeNewsId = "fatec.core.domain.home.news.id-{0}";
		private const string cacheFatecNewsAll = "fatec.core.domain.fatec.news";
		private const string cacheFatecNewsId = "fatec.core.domain.fatec.news.id-{0}";
		private const string cacheInternshipAll = "fatec.core.domain.internship";
		private const string cacheInternshipId = "fatec.core.domain.internship.id-{0}";

		private const int CACHE_MIN_EXPIRATION_TIME = 10;
		private const int CACHE_MAX_EXPIRATION_TIME = 1440;

		private readonly INewsRepository _newsRepository;
		private readonly ICacheManager _cacheStrategy;

		public NewsService(
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

			var cacheKey = string.Format(cacheHomeNewsId, id);

			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				var news = _newsRepository.GetSingleHomeNews(id);
				news.Subject = "h";

				return news;
			});
		}

		public ICollection<News> GetAllHomeNews()
		{
			var cacheKey = cacheHomeNewsAll;

			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _newsRepository.GetAllHomeNews();
				foreach (var aviso in avisosValidos)
					aviso.Subject = "h";

				return avisosValidos;
			});
		}

		#endregion

		#region Fatec

		public News GetFatecNews(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			var cacheKey = string.Format(cacheFatecNewsId, id);

			return _cacheStrategy.Get(cacheKey, CACHE_MAX_EXPIRATION_TIME, () =>
			{
				var news = _newsRepository.GetSingleFatecNews(id);
				news.Subject = "f";

				return news;
			});
		}

		public ICollection<News> GetAllFatecNews()
		{
			var cacheKey = cacheFatecNewsAll;

			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				var avisosValidos = _newsRepository.GetAllFatecNews();
				foreach (var aviso in avisosValidos)
					aviso.Subject = "f";

				return avisosValidos;
			});
		}

		#endregion

		#region Estagios

		public News GetInternship(int id)
		{
			if (id <= 0) throw new ArgumentOutOfRangeException("id", id, "id must be greather than ZERO.");

			string cacheKey = string.Format(cacheInternshipAll, id);

			return _cacheStrategy.Get(cacheKey, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetInternship(id);
			});
		}

		public ICollection<News> GetAllInternships()
		{
			return _cacheStrategy.Get(cacheInternshipId, CACHE_MIN_EXPIRATION_TIME, () =>
			{
				return _newsRepository.GetAllInternships();
			});
		}

		#endregion
	}
}
