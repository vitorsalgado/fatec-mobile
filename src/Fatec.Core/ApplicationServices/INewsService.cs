using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Services
{
	public interface INewsService
	{
		News GetInternship(int id);
		News GetFatecNews(int id);
		News GetHomeNews(int id);
		ICollection<News> GetAllInternships();
		ICollection<News> GetAllFatecNews();
		ICollection<News> GetAllHomeNews();
	}
}
