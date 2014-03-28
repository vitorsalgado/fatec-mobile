using Fatec.Core;
using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Core.Infrastructure.Mail;
using Fatec.Dependencies;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
			var dependencyResolver = IoC.GetResolver();
			DependencyResolver.SetResolver(dependencyResolver);

			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AutoMapperConfig.SetUp();

			//TaskManager.Instance.RunTasks();

			LogEvent("START");
		}

		protected void Application_End()
		{
			LogEvent("END");
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
		}

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			var culture = new CultureInfo("pt-BR");
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;

			if (!HttpContext.Current.Request.IsAuthenticated)
				return;

			var workContext = DependencyResolver.Current.GetService<IWorkContext>();
			var principal = new FatecPrincipal(workContext.CurrentUser, workContext.CurrentUser.Roles.ToArray());
			HttpContext.Current.User = principal;
		}

		private static void LogException(Exception exception)
		{
			var logService = DependencyResolver.Current.GetService<ILogService>();
			var workContext = DependencyResolver.Current.GetService<IWorkContext>();
			var httpContext = DependencyResolver.Current.GetService<HttpContextBase>();

			var log = new Log();
			log.CreatedOn = DateTime.Now;
			log.Message = exception.Message;
			log.Url = httpContext.Request == null ? string.Empty : httpContext.Request.Url.AbsoluteUri;
			log.IpAddress = httpContext.Request.UserHostAddress;
			log.Username = workContext.CurrentUser.Name;
			log.Details = exception.ToString();

			try
			{
				logService.Log(log);
				SendEmailToDeveloper(log);
			}
			catch { }
		}

		private static void SendEmailToDeveloper(Log log)
		{
			var emailService = DependencyResolver.Current.GetService<IEmailService>();
			var configService = DependencyResolver.Current.GetService<IConfigurationProvider>();

			var emailAccount = new EmailAccount();
			emailAccount.Username = configService.EmailUsername;
			emailAccount.UseDefaultCredentials = configService.UseDefaultCredentialsForEmail;
			emailAccount.Port = configService.EmailPort;
			emailAccount.Password = configService.EmailPassword;
			emailAccount.Host = configService.EmailHost;
			emailAccount.EnableSsl = configService.UseSSLForEmail;
			emailAccount.Email = configService.EmailAccount;
			emailAccount.DisplayName = configService.EmailDisplayName;

			var mail = new Email();
			mail.From = configService.EmailAccount;
			mail.To = new string[] { configService.DeveloperEmailAddress };
			mail.Subject = "FATEC MOBILE - ERROR LOG";

			StringBuilder builder = new StringBuilder();
			builder
				.AppendFormat("Error on: {0}", DateTime.Now).AppendLine()
				.AppendFormat("Message: {0}", log.Message).AppendLine()
				.AppendFormat("User: {0}", log.Username).AppendLine()
				.Append("-----------------------------------------------------------------").AppendLine().AppendLine();

			var serializedLog = CommonHelper.SerializeToXml(log);

			builder.Append(serializedLog).AppendLine()
				.Append("-----------------------------------------------------------------")
				.AppendLine();

			mail.Body = builder.ToString();

			emailService.SendEmail(emailAccount, mail);
		}

		private static void LogEvent(string eventDescription)
		{
			try
			{
				var logService = DependencyResolver.Current.GetService<ILogService>();
				logService.Inform(eventDescription);
			}
			catch { }
		}
	}
}