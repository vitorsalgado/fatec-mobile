using System;
using System.Configuration;
using System.Xml;

namespace Fatec.Core.Infrastructure.Configuration
{
	public class FatecMobileConfig : IConfigurationSectionHandler
	{
		#region Properties

		public Uri SharepointDefaultUrl { get; private set; }

		public string SharepointUsername { get; private set; }

		public string SharepointPassword { get; private set; }

		public bool UseDefaultCredentialsForSharepointConnections { get; private set; }

		public string DomainName { get; private set; }

		public string DomainAdminUsername { get; private set; }

		public string DomainAdminPassword { get; private set; }

		public int CacheDefaultExpirationTime { get; private set; }

		public string DeveloperEmailAddress { get; private set; }

		public string EmailDisplayName { get; private set; }

		public string EmailAccount { get; private set; }

		public string EmailUsername { get; private set; }

		public string EmailPassword { get; private set; }

		public string EmailHost { get; private set; }

		public int EmailPort { get; private set; }

		public bool UseSSLForEmail { get; private set; }

		public bool UseDefaultCredentialsForEmail { get; private set; }

		#endregion

		#region Builder

		public object Create(object parent, object configContext, XmlNode section)
		{
			var config = new FatecMobileConfig();

			BuildSharepointSection(section, config);
			BuildDomainSection(section, config);
			BuildCacheSection(section, config);
			BuildDeveloperSection(section, config);
			BuildEmailSection(section, config);

			return config;
		}

		#endregion

		#region Section Builders

		private static void BuildDeveloperSection(XmlNode section, FatecMobileConfig config)
		{
			var developerNode = section.SelectSingleNode("Developer");

			if (developerNode == null)
				throw new FatecException("Developer informations configuration section was not defined!");

			if (developerNode.Attributes["Email"] != null)
			{
				var attribute = developerNode.Attributes["Email"];
				if (attribute != null)
					config.DeveloperEmailAddress = attribute.Value;
			}
		}

		private static void BuildCacheSection(XmlNode section, FatecMobileConfig config)
		{
			var cacheNode = section.SelectSingleNode("Cache");

			if (cacheNode == null)
				throw new FatecException("Cache configuration section was not defined!");

			if (cacheNode.Attributes["DefaultExpirationTime"] != null)
			{
				var attribute = cacheNode.Attributes["DefaultExpirationTime"];
				if (attribute != null)
					config.CacheDefaultExpirationTime = Convert.ToInt32(attribute.Value);
			}
		}

		private static void BuildDomainSection(XmlNode section, FatecMobileConfig config)
		{
			var domainNode = section.SelectSingleNode("Domain");

			if (domainNode == null)
				throw new FatecException("Domain configuration section was not defined!");

			if (domainNode.Attributes["Name"] != null)
			{
				var attribute = domainNode.Attributes["Name"];
				if (attribute != null)
					config.DomainName = attribute.Value;
			}

			if (domainNode.Attributes["Username"] != null)
			{
				var attribute = domainNode.Attributes["Username"];
				if (attribute != null)
					config.DomainAdminUsername = attribute.Value;
			}

			if (domainNode.Attributes["Password"] != null)
			{
				var attribute = domainNode.Attributes["Password"];
				if (attribute != null)
					config.DomainAdminPassword = attribute.Value;
			}
		}

		private static void BuildSharepointSection(XmlNode section, FatecMobileConfig config)
		{
			var sharePointNode = section.SelectSingleNode("Sharepoint");

			if (sharePointNode == null)
				throw new FatecException("Sharepoint configuration section was not defined!");

			if (sharePointNode.Attributes["DefaultUrl"] != null)
			{
				var attribute = sharePointNode.Attributes["DefaultUrl"];
				if (attribute != null)
					config.SharepointDefaultUrl = new Uri(attribute.Value);
			}

			if (sharePointNode.Attributes["Username"] != null)
			{
				var attribute = sharePointNode.Attributes["Username"];
				if (attribute != null)
					config.SharepointUsername = attribute.Value;
			}

			if (sharePointNode.Attributes["Password"] != null)
			{
				var attribute = sharePointNode.Attributes["Password"];
				if (attribute != null)
					config.SharepointPassword = attribute.Value;
			}

			if (sharePointNode.Attributes["UseDefaultCredentials"] != null)
			{
				var attribute = sharePointNode.Attributes["UseDefaultCredentials"];
				if (attribute != null)
					config.UseDefaultCredentialsForSharepointConnections = Convert.ToBoolean(attribute.Value);
			}
		}

		private static void BuildEmailSection(XmlNode section, FatecMobileConfig config)
		{
			var emailNode = section.SelectSingleNode("Email");

			if (emailNode == null)
				throw new FatecException("Email configuration section was not defined!");

			if (emailNode.Attributes["DisplayName"] != null)
			{
				var attribute = emailNode.Attributes["DisplayName"];
				if (attribute != null)
					config.EmailDisplayName = attribute.Value;
			}

			if (emailNode.Attributes["Account"] != null)
			{
				var attribute = emailNode.Attributes["Account"];
				if (attribute != null)
					config.EmailAccount = attribute.Value;
			}

			if (emailNode.Attributes["Username"] != null)
			{
				var attribute = emailNode.Attributes["Username"];
				if (attribute != null)
					config.EmailUsername = attribute.Value;
			}

			if (emailNode.Attributes["Password"] != null)
			{
				var attribute = emailNode.Attributes["Password"];
				if (attribute != null)
					config.EmailPassword = attribute.Value;
			}

			if (emailNode.Attributes["Host"] != null)
			{
				var attribute = emailNode.Attributes["Host"];
				if (attribute != null)
					config.EmailHost = attribute.Value;
			}

			if (emailNode.Attributes["Port"] != null)
			{
				var attribute = emailNode.Attributes["Port"];
				if (attribute != null)
					config.EmailPort = Convert.ToInt32(attribute.Value);
			}

			if (emailNode.Attributes["SSL"] != null)
			{
				var attribute = emailNode.Attributes["SSL"];
				if (attribute != null)
					config.UseSSLForEmail = Convert.ToBoolean(attribute.Value);
			}

			if (emailNode.Attributes["UseDefaultCredentials"] != null)
			{
				var attribute = emailNode.Attributes["UseDefaultCredentials"];
				if (attribute != null)
					config.UseDefaultCredentialsForEmail = Convert.ToBoolean(attribute.Value);
			}
		}

		#endregion
	}
}
