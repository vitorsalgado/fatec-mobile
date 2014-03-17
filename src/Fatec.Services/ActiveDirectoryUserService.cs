using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Services;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Fatec.Services
{
	public class ActiveDirectoryUserService : IUserService
	{
		private static string[] DefaultSearchProperties = { "name", "givenName", "sn", "sAMAccountName", "userName", "description", "displayname", "memberOf", "distinguishedname", "userAccountControl" };
		private readonly IConfigurationProvider _configurationProvider;
		private const string LDAP_SCHEME = "LDAP://";

		public ActiveDirectoryUserService(IConfigurationProvider configurationProvider)
		{
			if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");
			_configurationProvider = configurationProvider;
		}

		private DirectoryEntry AdminDirectoryEntry
		{
			get { return new DirectoryEntry(LDAP_SCHEME + _configurationProvider.DomainName, _configurationProvider.DomainAdminUsername, _configurationProvider.DomainAdminPassword); }
		}

		public FatecIdentity GetByUsername(string username)
		{
			if (string.IsNullOrEmpty(username)) throw new ArgumentException("username");

			using (DirectoryEntry directoryEntry = AdminDirectoryEntry)
			{
				using (DirectorySearcher search = CreateSearcher(directoryEntry))
				{
					search.PropertiesToLoad.AddRange(DefaultSearchProperties);
					search.Filter = "(sAMAccountName=" + username + ")";
					var result = search.FindOne();

					string login = string.Empty;
					string fullName = string.Empty;
					string email = string.Empty;
					string[] roles;

					if (result.Properties["samAccountName"].Count > 0)
						login = result.Properties["samAccountName"][0].ToString();
					if (result.Properties["name"].Count > 0)
						fullName = result.Properties["name"][0].ToString();
					if (result.Properties["mail"].Count > 0)
						email = result.Properties["mail"][0].ToString();

					roles = this.GetRolesByUsername(login).ToArray();

					FatecIdentity user =
						new FatecIdentity(login, fullName, email, roles);

					return user;
				}
			}
		}

		private ICollection<string> GetRolesByUsername(string username)
		{
			ICollection<string> groupCollection = new List<string>();

			using (var directoryEntry = AdminDirectoryEntry)
			{
				using (var search = CreateSearcher(directoryEntry))
				{
					search.PropertiesToLoad.Add("memberOf");
					search.Filter = "(sAMAccountName=" + username + ")";
					var result = search.FindOne();
					int propertyCount = result.Properties["memberOf"].Count;
					string dn;
					int equalsIndex, commaIndex;
					for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
					{
						dn = (String)result.Properties["memberOf"][propertyCounter];
						equalsIndex = dn.IndexOf("=", 1);
						commaIndex = dn.IndexOf(",", 1);

						if (-1 == equalsIndex)
							return null;

						string group = dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1);
						groupCollection.Add(group);
					}
				}
			}

			return groupCollection;
		}

		public bool ValidateUser(string username, string password)
		{
			if (string.IsNullOrEmpty(username)) throw new ArgumentException("username");

			bool authenticated = false;
			DirectoryEntry entry = null;

			try
			{
				entry = new DirectoryEntry(LDAP_SCHEME + _configurationProvider.DomainName, username, password);
				object nativeObject = entry.NativeObject;
				authenticated = true;
			}
			catch (DirectoryServicesCOMException)
			{
				authenticated = false;
			}
			finally
			{
				if (entry != null)
					entry.Close();
			}

			return authenticated;
		}

		private static DirectorySearcher CreateSearcher(DirectoryEntry directoryEntry)
		{
			return new DirectorySearcher(directoryEntry);
		}
	}
}
