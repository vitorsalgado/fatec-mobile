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
			_configurationProvider = configurationProvider;
		}

		private DirectoryEntry AdminDirectoryEntry
		{
			get { return new DirectoryEntry(LDAP_SCHEME + _configurationProvider.DomainName, _configurationProvider.DomainAdminUsername, _configurationProvider.DomainAdminPassword); }
		}

		public SysUser GetByUsername(string username)
		{
			using (DirectoryEntry directoryEntry = AdminDirectoryEntry)
			{
				using (DirectorySearcher search = CreateSearcher(directoryEntry))
				{
					search.PropertiesToLoad.AddRange(DefaultSearchProperties);
					search.Filter = "(sAMAccountName=" + username + ")";
					var result = search.FindOne();
					SysUser user = null;
					if (result != null)
						user = Map(result);
					this.GetRolesByUsername(user.Username)
						.ToList()
						.ForEach(x => user.SysRoles.Add(x));
					return user;
				}
			}
		}

		private ICollection<SysRole> GetRolesByUsername(string username)
		{
			ICollection<SysRole> groupCollection = new List<SysRole>();

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
						SysRole group = new SysRole();
						group.Name = dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1);
						groupCollection.Add(group);
					}
				}
			}

			return groupCollection;
		}

		public bool ValidateUser(string username, string password)
		{
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
				if(entry != null)
					entry.Close();
			}

			return authenticated;
		}

		private static SysUser Map(SearchResult searchResult)
		{
			SysUser user = new SysUser();

			if (searchResult.Properties["samAccountName"].Count > 0)
				user.Username = searchResult.Properties["samAccountName"][0].ToString();
			if (searchResult.Properties["name"].Count > 0)
				user.Fullname = searchResult.Properties["name"][0].ToString();
			if (searchResult.Properties["mail"].Count > 0)
				user.Email = searchResult.Properties["mail"][0].ToString();

			return user;
		}

		private static DirectorySearcher CreateSearcher(DirectoryEntry directoryEntry)
		{
			return new DirectorySearcher(directoryEntry);
		}
	}
}
