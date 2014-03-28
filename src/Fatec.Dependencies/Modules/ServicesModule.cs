using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Services;
using Fatec.Services;
using System;

namespace Fatec.Dependencies.Modules
{
	public class ServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.RegisterType<NewsService>().As<INewsService>().InstancePerHttpRequest();
			builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerHttpRequest();
			builder.RegisterType<ActiveDirectoryUserService>().As<IUserService>().InstancePerHttpRequest();
			builder.RegisterType<FatecService>().As<IFatecService>().InstancePerHttpRequest();
			builder.RegisterType<StudentService>().As<IStudentService>().InstancePerHttpRequest();
			builder.RegisterType<DisciplineService>().As<IDisciplineService>().InstancePerHttpRequest();
		}
	}
}
