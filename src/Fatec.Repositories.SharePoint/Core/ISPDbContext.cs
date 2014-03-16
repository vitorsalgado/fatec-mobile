using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SharepointListsService = Fatec.Repositories.SharePoint.Lists.Lists;

namespace Fatec.Repositories.SharePoint
{
	public interface ISPDbContext
	{
		SharepointListsService CreateConnectionToListsService(string siteRelativePath);
		string CreateViewFieldsNode(params string[] fields);
		ICollection<T> ExecuteQuery<T>(string sitePath, string listName, string query, string viewFields, Func<XElement, T> map);
		ICollection<T> ExecuteQuery<T>(string sitePath, string listName, string query, string viewFields, Func<XElement, T> map, int rowLimit);
	}
}
