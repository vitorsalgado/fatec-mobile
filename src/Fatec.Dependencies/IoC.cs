using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Dependencies.Modules;
using System.Web.Mvc;

namespace Fatec.Dependencies
{
	public static class IoC
	{
		public static IDependencyResolver GetResolver()
		{
			return new AutofacDependencyResolver(BuildAutofacContainer());
		}

		private static IContainer BuildAutofacContainer()
		{
			var builder = new ContainerBuilder();

			RegisterModules(builder);
			
			return builder.Build();
		}

		private static void RegisterModules(ContainerBuilder builder)
		{
			builder.RegisterModule(new WebComponentsModule());
			builder.RegisterModule(new InfrastructureComponentsModule());
			builder.RegisterModule(new RepositoriesModule());
			builder.RegisterModule(new ServicesModule());
		}
	}
}
