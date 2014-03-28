using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Repositories;
using Fatec.Repositories.MySql;
using Fatec.Repositories.SharePoint;
using System;
using System.Data.Entity;

namespace Fatec.Dependencies.Modules
{
	public class RepositoriesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			RegisterTypesForSharepoint(builder);
			RegisterTypesForMySql(builder);
		}

		private static void RegisterTypesForSharepoint(ContainerBuilder builder)
		{
			builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerHttpRequest();
			builder.RegisterType<FatecRepository>().As<IFatecRepository>().InstancePerHttpRequest();
			builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerHttpRequest();
			builder.RegisterType<DisciplineRepository>().As<IDisciplineRepository>().InstancePerHttpRequest();
			builder.RegisterType<NewsRepository>().As<INewsRepository>().InstancePerHttpRequest();
			builder.RegisterType<SPDbContext>().As<ISPDbContext>().InstancePerHttpRequest();
		}

		private static void RegisterTypesForMySql(ContainerBuilder builder)
		{
			builder.RegisterType<FatecDbContext>().As<DbContext>().InstancePerHttpRequest();
			builder.RegisterType<LogRepository>().As<ILogRepository>().InstancePerHttpRequest();
			builder.RegisterType<ApplicationEventRepository>().As<IApplicationEventsRepository>().InstancePerHttpRequest();
		}
	}
}
