using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Tasks;
using Fatec.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fatec.MobileUI.Infrastructure.Tasks
{
	public class CacheTask : ITask
	{
		private const string CACHE_DISCIPLINE_BY_ID = "fatec.domain.discipline-{0}";

		private const string cacheHomeNewsAll = "fatec.core.domain.home.news";
		private const string cacheHomeNewsId = "fatec.core.domain.home.news.id-{0}";
		private const string cacheFatecNewsAll = "fatec.core.domain.fatec.news";
		private const string cacheFatecNewsId = "fatec.core.domain.fatec.news.id-{0}";
		private const string cacheInternshipAll = "fatec.core.domain.internship";
		private const string cacheInternshipId = "fatec.core.domain.internship.id-{0}";

		private const int CACHE_LONG_EXPIRATION = 2440;
		private const int CACHE_MEDIUM_EXPIRATION = 1440;

		public string Description
		{
			get { return "Fill cache."; }
		}

		public void Run()
		{
			if (HttpContext.Current == null)
				return;

			var cacheManager = DependencyResolver.Current.GetService<ICacheManager>();
			var disciplineService = DependencyResolver.Current.GetService<IDisciplineService>();
			var newsService = DependencyResolver.Current.GetService<INewsService>();
			var studentService = DependencyResolver.Current.GetService<IStudentService>();

			cacheManager.ClearAll();

			ICollection<Discipline> disciplines = null;
			var disciplineTask = Task.Factory.StartNew(() =>
			{
				disciplines = disciplineService.GetAllDisciplines();
			});

			ICollection<News> fatecAnnouncements = null;
			var fatecAnnouncementsTask = Task.Factory.StartNew(() =>
			{
				fatecAnnouncements = newsService.GetAllFatecNews();
			});

			ICollection<News> homeAnnouncements = null;
			var homeAnnouncementsTask = Task.Factory.StartNew(() =>
			{
				homeAnnouncements = newsService.GetAllHomeNews();
			});

			Task.WaitAll(disciplineTask, fatecAnnouncementsTask, homeAnnouncementsTask);

			Parallel.ForEach(disciplines, x =>
			{
				string key = string.Format(CACHE_DISCIPLINE_BY_ID, x.Id);
				cacheManager.Add(key, x, CACHE_LONG_EXPIRATION);
			});

			Parallel.ForEach(fatecAnnouncements, x =>
			{
				string key = string.Format(cacheFatecNewsId, x.Id);
				cacheManager.Add(key, x, CACHE_MEDIUM_EXPIRATION);
			});

			Parallel.ForEach(homeAnnouncements, x =>
			{
				string key = string.Format(cacheHomeNewsId, x.Id);
				cacheManager.Add(key, x, CACHE_MEDIUM_EXPIRATION);
			});
		}
	}
}
