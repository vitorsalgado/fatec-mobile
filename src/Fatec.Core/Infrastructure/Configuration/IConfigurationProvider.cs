using System;

namespace Fatec.Core.Infrastructure.Configuration
{
	public interface IConfigurationProvider
	{
		string DomainName { get; }
		string DomainAdminUsername { get; }
		string DomainAdminPassword { get; }

		Uri SharepointDefaultUrl { get; }
		string SharepointUsername { get; }
		string SharepointPassword { get; }
		bool UseDefaultCredentialsForSharepointConnections { get; }

		int DefaultCacheExpirationTime { get; }

		string EmailDisplayName { get; }
		string EmailAccount { get; }
		string EmailUsername { get; }
		string EmailPassword { get; }
		string EmailHost { get; }
		int EmailPort { get; }
		bool UseSSLForEmail { get; }
		bool UseDefaultCredentialsForEmail { get; }

		string DeveloperEmailAddress { get; }
	}
}
