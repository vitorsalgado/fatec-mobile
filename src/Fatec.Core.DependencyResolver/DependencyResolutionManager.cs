using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Infrastructure;
using Fatec.Core.Infrastructure.Caching;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Core.Repositories;
using Fatec.Core.Services;
using Fatec.Infrastructure.Caching;
using Fatec.Infrastructure.Configuration;
using Fatec.Infrastructure.Logger;
using Fatec.Repository.SharePoint;
using Fatec.Services;
using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Fatec.Core.DependencyResolver
{
	public static class DependencyResolutionManager
	{
		public static IDependencyResolver GetResolver()
		{
			var container = BuildAutofacContainer();
			return new AutofacDependencyResolver(container);
		}

		private static IContainer BuildAutofacContainer()
		{
			var builder = new ContainerBuilder();
			RegisterTypes(builder);
			var container = builder.Build();
			return container;
		}

		private static void RegisterTypes(ContainerBuilder builder)
		{
			RegisterWebComponents(builder);
			RegisterInfrastructure(builder);
			RegisterRepositories(builder);
			RegisterServices(builder);
		}

		private static void RegisterWebComponents(ContainerBuilder builder)
		{
			builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase).As<HttpContextBase>().InstancePerHttpRequest();
			builder.RegisterControllers(GetAssemblies());
			builder.RegisterType<FatecWorkContext>().As<IWorkContext>().InstancePerHttpRequest();
		}

		private static void RegisterInfrastructure(ContainerBuilder builder)
		{
			builder.RegisterType<NLogLogger>().As<ILogger>().SingleInstance();
			builder.RegisterType<MemoryCacheStrategy>().As<ICacheStrategy>().InstancePerHttpRequest();
			builder.RegisterType<WebConfigurationProvider>().As<IConfigurationProvider>().SingleInstance();
			builder.RegisterType<FatecEngine>().As<IEngine>().SingleInstance();
		}

		private static void RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterType<AnnouncementsServiceFacade>().As<IAnnouncementService>().InstancePerHttpRequest();
			builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerHttpRequest();
			builder.RegisterType<ActiveDirectoryUserService>().As<IUserService>().InstancePerHttpRequest();
			builder.RegisterType<EmailService>().As<IEmailService>().InstancePerHttpRequest();
			builder.RegisterType<FatecService>().As<IFatecService>().InstancePerHttpRequest();
		}

		private static void RegisterRepositories(ContainerBuilder builder)
		{
			builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerHttpRequest();
			builder.RegisterType<DiretorioAcademicoRepository>().As<IAcademicDirectoryAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<FatecRepository>().As<IFatecRepository>().InstancePerHttpRequest();
			builder.RegisterType<HomeRepository>().As<IHomeAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<TraineeRepository>().As<ITraineeRepository>().InstancePerHttpRequest();
			builder.RegisterType<TIAnnouncementsRepository>().As<ITIAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerHttpRequest();
			builder.RegisterType<ClassAssignmentRepository>().As<IClassAssignmentRepository>().InstancePerHttpRequest();
		}

		private static Assembly[] GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.AsQueryable()
				.Where(x => x.FullName.Contains("Fatec"))
				.ToArray();
		}
	}
}