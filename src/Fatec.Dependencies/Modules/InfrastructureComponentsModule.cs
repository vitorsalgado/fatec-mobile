using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Core.Infrastructure.Mail;
using Fatec.Infrastructure.Caching;
using Fatec.Infrastructure.Configuration;
using Fatec.Infrastructure.Logger;
using Fatec.Infrastructure.Mail;
using System;

namespace Fatec.Dependencies.Modules
{
	public class InfrastructureComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerHttpRequest();
			builder.RegisterType<WebConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
			builder.RegisterType<LogService>().As<ILogService>().InstancePerHttpRequest();
			builder.RegisterType<EmailService>().As<IEmailService>().InstancePerHttpRequest();
		}
	}
}
