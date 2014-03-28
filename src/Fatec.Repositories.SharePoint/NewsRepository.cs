using Fatec.Core.Domain;
using Fatec.Core.Repositories;
using System.Collections.Generic;

namespace Fatec.Repositories.SharePoint
{
	public class NewsRepository : AbstractNewsRepository, INewsRepository
	{
		private const string fatecListPath = "/fatec";
		private const string homeListPath = "/";
		private const string internshipListPath = "/estagio";

		private const string listName = "Avisos";
		private const string internshipListname = "Oportunidades";

		public NewsRepository(ISPDbContext context)
			: base(context) { }

		public News GetSingleHomeNews(int id)
		{
			return base.GetNewsById(id, listName, homeListPath);
		}

		public ICollection<News> GetAllHomeNews()
		{
			return base.GetValidNews(listName, homeListPath);
		}

		public News GetSingleFatecNews(int id)
		{
			return base.GetNewsById(id, listName, fatecListPath);
		}

		public ICollection<News> GetAllFatecNews()
		{
			return base.GetValidNews(listName, fatecListPath);
		}

		public News GetInternship(int id)
		{
			return base.GetNewsById(id, internshipListname, internshipListPath);
		}

		public ICollection<News> GetAllInternships()
		{
			return base.GetValidNews(internshipListname, internshipListPath);
		}
	}
}
