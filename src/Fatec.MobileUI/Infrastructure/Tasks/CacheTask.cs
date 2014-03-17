using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Tasks;
using Fatec.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fatec.MobileUI.Infrastructure.Tasks
{
	public class CacheTask : ITask
	{
		private const string CACHE_DISCIPLINE_BY_ID = "fatec.domain.disciplina-{0}";
		private const string CACHE_AVISO_FATEC_ID = "fatec.avisofatec.id-{0}";
		private const string CACHE_AVISO_HOME_ID = "fatec.avisohome.id-{0}";

		private const int CACHE_LONG_EXPIRATION = 2440;
		private const int CACHE_MEDIUM_EXPIRATION = 1440;

		public string Description
		{
			get { return "Fill cache."; }
		}

		public void Run()
		{
			var cacheManager = DependencyResolver.Current.GetService<ICacheManager>();

			var disciplineService = DependencyResolver.Current.GetService<IDisciplineService>();
			ICollection<Discipline> disciplines = null;
			var disciplineTask = Task.Factory.StartNew(() =>
			{
				disciplines = disciplineService.GetAllDisciplines();
			});

			var announcementsFacade = DependencyResolver.Current.GetService<IAnnouncementService>();
			ICollection<Announcement> fatecAnnouncements = null;
			var fatecAnnouncementsTask = Task.Factory.StartNew(() =>
			{
				fatecAnnouncements = announcementsFacade.GetFatecValidAnnouncements();
			});

			ICollection<Announcement> homeAnnouncements = null;
			var homeAnnouncementsTask = Task.Factory.StartNew(() =>{
				homeAnnouncements = announcementsFacade.GetHomeValidAnnouncements();
			});

			Task.WaitAll(disciplineTask, fatecAnnouncementsTask, homeAnnouncementsTask);

			Parallel.ForEach(disciplines, x =>
			{
				string key = string.Format(CACHE_DISCIPLINE_BY_ID, x.Id);
				cacheManager.Add(key, x, CACHE_LONG_EXPIRATION);
			});

			Parallel.ForEach(fatecAnnouncements, x =>
			{
				string key = string.Format(CACHE_AVISO_FATEC_ID, x.Id);
				cacheManager.Add(key, x, CACHE_MEDIUM_EXPIRATION);
			});

			Parallel.ForEach(homeAnnouncements, x =>
			{
				string key = string.Format(CACHE_AVISO_HOME_ID, x.Id);
				cacheManager.Add(key, x, CACHE_MEDIUM_EXPIRATION);
			});
		}
	}
}