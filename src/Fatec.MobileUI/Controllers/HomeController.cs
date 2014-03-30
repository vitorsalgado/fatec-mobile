using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Configuration;
using Fatec.Core.Infrastructure.Mail;
using Fatec.MobileUI.ViewModels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace Fatec.MobileUI.Controllers
{
	[AllowAnonymous]
	public class HomeController : Controller
	{
		private IEmailService _emailService;
		private IConfigurationProvider _configService;

		public HomeController(IEmailService emailService, IConfigurationProvider configurationProvider)
		{
			_emailService = emailService;
			_configService = configurationProvider;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Sobre()
		{
			return View();
		}

		public ActionResult Feedback()
		{
			var model = new FeedbackModel();
			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> Feedback(FeedbackModel model)
		{
			if (ModelState.IsValid)
			{
				var emailAccount = new EmailAccount();
				emailAccount.Username = _configService.EmailUsername;
				emailAccount.UseDefaultCredentials = _configService.UseDefaultCredentialsForEmail;
				emailAccount.Port = _configService.EmailPort;
				emailAccount.Password = _configService.EmailPassword;
				emailAccount.Host = _configService.EmailHost;
				emailAccount.EnableSsl = _configService.UseSSLForEmail;
				emailAccount.Email = _configService.EmailAccount;
				emailAccount.DisplayName = _configService.EmailDisplayName;

				var mail = new Email();
				mail.From = _configService.EmailAccount;
				mail.To = new string[] { _configService.DeveloperEmailAddress };
				mail.Subject = "FATEC MOBILE - FEEDBACK";

				StringBuilder builder = new StringBuilder();
				builder.Append("Avaliação: ").Append(model.Rate).AppendLine()
					.Append("Comentários: ").AppendLine()
					.Append(model.Comments);

				mail.Body = builder.ToString();

				await Task.Run(() => _emailService.SendEmail(emailAccount, mail));

				return View("FeedbackSentConfirmation");
			}

			return View();
		}
	}
}
