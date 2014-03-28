using Fatec.Core.Infrastructure;
using Fatec.Core.Infrastructure.Configuration;
using System;
using System.Configuration;

namespace Fatec.Infrastructure.Configuration
{
	public class WebConfigurationProvider : IConfigurationProvider
	{
		private readonly FatecMobileConfig _config;

		public WebConfigurationProvider()
		{
			_config = ConfigurationManager.GetSection("FatecMobileConfig") as FatecMobileConfig;
			if (_config == null) throw new FatecException("\"FatecMobileConfig\" configuration section has not been defined.");
		}

		public string DomainName
		{
			get { return _config.DomainName; }
		}

		public string DomainAdminUsername
		{
			get { return _config.DomainAdminUsername; }
		}

		public string DomainAdminPassword
		{
			get { return _config.DomainAdminPassword; }
		}

		public Uri SharepointDefaultUrl
		{
			get { return _config.SharepointDefaultUrl; }
		}

		public string SharepointUsername
		{
			get { return _config.SharepointUsername; }
		}

		public string SharepointPassword
		{
			get { return _config.SharepointPassword; }
		}

		public bool UseDefaultCredentialsForSharepointConnections
		{
			get { return _config.UseDefaultCredentialsForSharepointConnections; }
		}

		public int DefaultCacheExpirationTime
		{
			get { return _config.CacheDefaultExpirationTime; }
		}

		public string EmailDisplayName
		{
			get { return _config.EmailDisplayName; }
		}

		public string EmailAccount
		{
			get { return _config.EmailAccount; }
		}

		public string EmailUsername
		{
			get { return _config.EmailUsername; }
		}

		public string EmailPassword
		{
			get { return _config.EmailPassword; }
		}

		public string EmailHost
		{
			get { return _config.EmailHost; }
		}

		public int EmailPort
		{
			get { return _config.EmailPort; }
		}

		public bool UseSSLForEmail
		{
			get { return _config.UseSSLForEmail; }
		}

		public bool UseDefaultCredentialsForEmail
		{
			get { return _config.UseDefaultCredentialsForEmail; }
		}

		public string DeveloperEmailAddress
		{
			get { return _config.DeveloperEmailAddress; }
		}
	}
}
