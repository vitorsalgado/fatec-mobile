using Fatec.Core.Domain;
using Fatec.Repositories.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fatec.Repositories.SharePoint
{
	public abstract class BaseAnnouncementsRepository
	{
		protected ISPDbContext _context;

		protected BaseAnnouncementsRepository(ISPDbContext context)
		{
			if (context == null)
				throw new ArgumentNullException("context");
			_context = context;
		}

		protected abstract string AnnouncementsListPath { get; }

		protected abstract string AnnouncementsListName { get; }

		public virtual Announcement Get(int id)
		{
			string query = string.Format(
				@"<Where><Eq><FieldRef Name='ID'/>
					<Value Type='Text'>{0}</Value></Eq></Where>
				<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>", id);
			string viewFields = _context.CreateViewFieldsNode("ID", "Title", "Body", "Expires", "Author", "Created");

			return _context.ExecuteQuery<Announcement>(AnnouncementsListPath, AnnouncementsListName, query, viewFields, AnnouncementMap.Map, 1).FirstOrDefault();
		}

		public virtual ICollection<Announcement> GetAllValid()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Expires'/><Value Type='DateTime'><Today /></Value></Geq>
				</Where><OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>";
			string viewFields = _context.CreateViewFieldsNode("ID", "Title", "Body", "Expires", "Author", "Created");

			return _context.ExecuteQuery<Announcement>(AnnouncementsListPath, AnnouncementsListName, query, viewFields, AnnouncementMap.Map);
		}
	}
}
