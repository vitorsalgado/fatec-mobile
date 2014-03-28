using Fatec.Core.Domain;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fatec.Repositories.SharePoint
{
	public abstract class AbstractNewsRepository
	{
		private readonly ISPDbContext _context;
		protected ISPDbContext context { get { return _context; } }

		protected AbstractNewsRepository(ISPDbContext context)
		{
			if (context == null) throw new ArgumentNullException("context");
			_context = context;
		}

		protected News GetNewsById(int newsId, string listName, string listPath)
		{
			if (newsId <= 0) throw new ArgumentOutOfRangeException("newsId");
			if (string.IsNullOrEmpty(listName)) throw new ArgumentNullException("listName");
			if (string.IsNullOrEmpty(listPath)) throw new ArgumentNullException(listPath);

			string query = string.Format(
				@"<Where><Eq><FieldRef Name='ID'/>
					<Value Type='Text'>{0}</Value></Eq></Where>
				<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>", newsId);
			string viewFields = _context.CreateViewFields("ID", "Title", "Body", "Expires", "Author", "Created");

			return _context.ExecuteQuery<News>(
				listPath, listName, query, viewFields, NewsMap.Map, 1).FirstOrDefault();
		}

		protected ICollection<News> GetValidNews(string listName, string listPath)
		{
			if (string.IsNullOrEmpty(listName)) throw new ArgumentNullException("listName");
			if (string.IsNullOrEmpty(listPath)) throw new ArgumentNullException(listPath);

			string query =
				@"<Where><Geq><FieldRef Name='Expires'/><Value Type='DateTime'><Today /></Value></Geq>
				</Where><OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>";
			string viewFields = _context.CreateViewFields("ID", "Title", "Body", "Expires", "Author", "Created");

			return _context.ExecuteQuery<News>(
				listPath, listName, query, viewFields, NewsMap.Map);
		}
	}
}
