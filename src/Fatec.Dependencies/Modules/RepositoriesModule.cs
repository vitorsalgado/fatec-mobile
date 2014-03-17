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
			builder.RegisterType<DiretorioAcademicoRepository>().As<IAcademicDirectoryAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<FatecRepository>().As<IFatecRepository>().InstancePerHttpRequest();
			builder.RegisterType<HomeRepository>().As<IHomeAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<TraineeRepository>().As<ITraineeRepository>().InstancePerHttpRequest();
			builder.RegisterType<TIAnnouncementsRepository>().As<ITIAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerHttpRequest();
			builder.RegisterType<ClassAssignmentRepository>().As<IClassAssignmentRepository>().InstancePerHttpRequest();
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
