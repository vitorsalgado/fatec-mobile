using Fatec.Core.Domain;
using Fatec.Repository.Mapping;
using System.Collections.Generic;
using System.Linq;

namespace Fatec.Repository.SharePoint
{
	public abstract class BaseAnnouncementsRepository
	{
		protected abstract string AnnouncementsListPath { get; }

		public abstract string AnnouncementsListName { get; }

		public virtual Announcement Get(int id)
		{
			string query = string.Format(
				@"<Where><Eq><FieldRef Name='ID'/>
					<Value Type='Text'>{0}</Value></Eq></Where>
				<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>", id);
			string viewFields = SPDb.CreateViewFieldsNode("ID", "Title", "Body", "Expires", "Author", "Created");

			return SPDb.ExecuteQuery<Announcement>(AnnouncementsListPath, AnnouncementsListName, query, viewFields, AnnouncementMap.Map, 1).FirstOrDefault();
		}

		public virtual ICollection<Announcement> GetAllValid()
		{
			string query =
				@"<Where><Geq><FieldRef Name='Expires'/><Value Type='DateTime'><Today /></Value></Geq>
				</Where><OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>";
			string viewFields = SPDb.CreateViewFieldsNode("ID", "Title", "Body", "Expires", "Author", "Created");

			return SPDb.ExecuteQuery<Announcement>(AnnouncementsListPath, AnnouncementsListName, query, viewFields, AnnouncementMap.Map);
		}
	}
}
