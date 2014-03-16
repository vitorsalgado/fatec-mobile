using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Infrastructure.Caching;
using Fatec.Infrastructure.Configuration;
using Fatec.Infrastructure.Logger;

namespace Fatec.Dependencies.Modules
{
	public class InfrastructureComponentsModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<NLogLogger>().As<IFileSystemLogger>().SingleInstance();
			builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().InstancePerHttpRequest();
			builder.RegisterType<WebConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
		}
	}
}
