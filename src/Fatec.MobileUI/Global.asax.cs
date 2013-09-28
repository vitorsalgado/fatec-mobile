using Fatec.Core;
using Fatec.Core.DependencyResolver;
using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Core.Services;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fatec.MobileUI
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			var dependencyResolver = DependencyResolutionManager.GetResolver();
			DependencyResolver.SetResolver(dependencyResolver);

			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			//TaskManager.RunStartupTasks();
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			var exception = Server.GetLastError();

			if (exception == null)
				return;

			var httpException = exception as HttpException;
			if (httpException != null && httpException.GetHttpCode() == 404)
				return;

			LogException(exception);
			SendEmailToDeveloper(exception);
		}

		private static void LogException(Exception exception)
		{
			var logger = DependencyResolver.Current.GetService<ILogger>();
			var workContext = DependencyResolver.Current.GetService<IWorkContext>();
			var httpContext = DependencyResolver.Current.GetService<HttpContextBase>();

			logger.Log(new Log()
			{
				CreatedOn = DateTime.Now,
				Exception = exception,
				LogLevel = LogLevel.Error,
				Message = "Application Exception",
				Details = exception.ToString(),
				ReferrerUrl = httpContext.Request.UrlReferrer == null ? string.Empty : httpContext.Request.UrlReferrer.PathAndQuery,
				RawUrl = httpContext.Request == null ? string.Empty : httpContext.Request.RawUrl,
				IpAddress = httpContext.Request.UserHostAddress,
				User = workContext.CurrentUser
			});
		}

		private void SendEmailToDeveloper(Exception exception)
		{
			var emailService = DependencyResolver.Current.GetService<IEmailService>();
			var configService = DependencyResolver.Current.GetService<IConfigurationProvider>();

			var emailAccount = new EmailAccount()
			{
				Username = configService.EmailUsername,
				UseDefaultCredentials = configService.UseDefaultCredentialsForEmail,
				Port = configService.EmailPort,
				Password = configService.EmailPassword,
				Host = configService.EmailHost,
				EnableSsl = configService.UseSSLForEmail,
				Email = configService.EmailAccount,
				DisplayName = configService.EmailDisplayName
			};

			var mail = new Email()
			{
				From = configService.EmailAccount,
				To = new string[] { configService.DeveloperEmailAddress },
				Body = exception.ToString(),
				Subject = "My Apps - FATEC MOBILE - ERROR LOG"
			};

			emailService.SendEmail(emailAccount, mail);
		}
	}
}