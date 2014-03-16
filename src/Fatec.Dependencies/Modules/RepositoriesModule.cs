using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Repositories;
using Fatec.Repositories.SharePoint;

namespace Fatec.Dependencies.Modules
{
	public class RepositoriesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<StudentRepository>().As<IStudentRepository>().InstancePerHttpRequest();
			builder.RegisterType<DiretorioAcademicoRepository>().As<IAcademicDirectoryAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<FatecRepository>().As<IFatecRepository>().InstancePerHttpRequest();
			builder.RegisterType<HomeRepository>().As<IHomeAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<TraineeRepository>().As<ITraineeRepository>().InstancePerHttpRequest();
			builder.RegisterType<TIAnnouncementsRepository>().As<ITIAnnouncementsRepository>().InstancePerHttpRequest();
			builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerHttpRequest();
			builder.RegisterType<ClassAssignmentRepository>().As<IClassAssignmentRepository>().InstancePerHttpRequest();

			builder.RegisterType<SPDb>().As<ISPDbContext>().InstancePerHttpRequest();
		}
	}
}
