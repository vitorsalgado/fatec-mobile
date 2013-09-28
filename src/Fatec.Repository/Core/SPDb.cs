using Fatec.Core;
using Fatec.Core.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using SharepointListsService = Fatec.Repository.SharePoint.Lists.Lists;

namespace Fatec.Repository.SharePoint
{
	public static class SPDb
	{
		private static IConfigurationProvider _configurationProvider = EngineWrapper.Current.Resolve<IConfigurationProvider>();
		private const string VTI_BIN_LISTS_PATH = "/_vti_bin/Lists.asmx";
		private const string HTTPCONTEXT_LISTS_CLIENT_KEY = "fatec.repository.spdb.sharepoint-lists";

		public static SharepointListsService CreateConnectionToListsService(string siteRelativePath)
		{
			if (String.IsNullOrEmpty(siteRelativePath)) throw new ArgumentNullException("siteRelativePath");

			HttpContextBase httpContext = EngineWrapper.Current.Resolve<HttpContextBase>();
			if (httpContext.Items[HTTPCONTEXT_LISTS_CLIENT_KEY] != null)
				return (SharepointListsService)httpContext.Items[HTTPCONTEXT_LISTS_CLIENT_KEY];
			
			var client = new SharepointListsService();
			client.Url = string.Concat(_configurationProvider.SharepointDefaultUrl.AbsoluteUri, siteRelativePath, VTI_BIN_LISTS_PATH);

			if (_configurationProvider.UseDefaultCredentialsForSharepointConnections)
			{
				client.UseDefaultCredentials = true;
				client.Credentials = CredentialCache.DefaultNetworkCredentials;
			}
			else
				client.Credentials = new NetworkCredential(_configurationProvider.SharepointUsername, _configurationProvider.SharepointPassword, _configurationProvider.DomainName);

			httpContext.Items[HTTPCONTEXT_LISTS_CLIENT_KEY] = client;

			return client;
		}

		public static string CreateViewFieldsNode(params string[] fields)
		{
			if (fields.Length == 0)
				return "<ViewFields />";

			StringBuilder viewFields = new StringBuilder();

			foreach (var field in fields)
				viewFields.Append("<FieldRef Name='")
					.Append(field)
				.Append("'/>");

			return viewFields.ToString();
		}

		public static ICollection<T> ExecuteQuery<T>(string sitePath, string listName, string query, string viewFields, Func<XElement, T> map)
		{
			return ExecuteQuery<T>(sitePath, listName, query, viewFields, map, 0);
		}

		public static ICollection<T> ExecuteQuery<T>(string sitePath, string listName, string query, string viewFields, Func<XElement, T> map, int rowLimit)
		{
			if (string.IsNullOrEmpty(sitePath)) throw new ArgumentNullException("sitePath");
			if (string.IsNullOrEmpty(listName)) throw new ArgumentNullException("listName");
			if (map == null) throw new ArgumentNullException("map");
			if (rowLimit < 0) throw new ArgumentOutOfRangeException("rowLimit", rowLimit, "\"rowLimit\" must be greather or equal to ZERO.");

			if (string.IsNullOrEmpty(query)) query = "<Query />";
			if (String.IsNullOrEmpty(viewFields)) viewFields = "<ViewFields />";

			XmlDocument xmlDoc = NewXmlDocWithSPSchemeAndNamespaces;

			using (var service = CreateConnectionToListsService(sitePath))
			{
				var ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");
				ndQuery.InnerXml = query;
				var ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");
				ndViewFields.InnerXml = viewFields;

				var response = service.GetListItems(listName, null, ndQuery, ndViewFields, rowLimit.ToString(), null, null);

				XDocument xDoc = new XDocument();
				using (XmlWriter xmlWriter = xDoc.CreateWriter())
					response.WriteTo(xmlWriter);

				var result = xDoc.Descendants(XName.Get("row", "#RowsetSchema"))
					.Select(x => map(x))
					.ToList<T>();

				return result;
			}
		}

		private static XmlDocument _xmlDocWithSPSchemeAndNamespaces;
		private static XmlDocument NewXmlDocWithSPSchemeAndNamespaces
		{
			get
			{
				if (_xmlDocWithSPSchemeAndNamespaces == null)
				{
					_xmlDocWithSPSchemeAndNamespaces = new XmlDocument();
					XmlNamespaceManager namespaceManager = new XmlNamespaceManager(_xmlDocWithSPSchemeAndNamespaces.NameTable);
					namespaceManager.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");
					namespaceManager.AddNamespace("namespace", "http://schemas.microsoft.com/sharepoint/soap/");
					namespaceManager.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
				}
				return _xmlDocWithSPSchemeAndNamespaces;
			}
		}
	}
}
