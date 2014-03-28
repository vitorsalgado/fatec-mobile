using Fatec.Core.Domain;
using System.Collections.Generic;

namespace Fatec.Core.Repositories
{
	public interface INewsRepository
	{
		News GetSingleHomeNews(int id);
		ICollection<News> GetAllHomeNews();
		News GetSingleFatecNews(int id);
		ICollection<News> GetAllFatecNews();
		News GetInternship(int id);
		ICollection<News> GetAllInternships();
	}
}
