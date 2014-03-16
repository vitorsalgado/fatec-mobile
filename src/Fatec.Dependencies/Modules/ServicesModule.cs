﻿using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core.Services;
using Fatec.Services;

namespace Fatec.Dependencies.Modules
{
	public class ServicesModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<AnnouncementsServiceFacade>().As<IAnnouncementService>().InstancePerHttpRequest();
			builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerHttpRequest();
			builder.RegisterType<ActiveDirectoryUserService>().As<IUserService>().InstancePerHttpRequest();
			builder.RegisterType<EmailService>().As<IEmailService>().InstancePerHttpRequest();
			builder.RegisterType<FatecService>().As<IFatecService>().InstancePerHttpRequest();
			builder.RegisterType<StudentService>().As<IStudentService>().InstancePerHttpRequest();
		}
	}
}
