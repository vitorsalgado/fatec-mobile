using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Mail;
using System;
using System.Net;
using System.Net.Mail;

namespace Fatec.Infrastructure.Mail
{
	public class EmailService : IEmailService
	{
		public void SendEmail(EmailAccount account, Email email)
		{
			if (account == null) throw new ArgumentNullException("account");
			if (email == null) throw new ArgumentNullException("email");

			using (var message = new MailMessage())
			{
				message.From = new MailAddress(email.From, account.DisplayName);

				foreach (var address in email.To)
					message.To.Add(address.Trim());

				foreach (var address in email.Cc)
					message.CC.Add(address.Trim());

				foreach (var address in email.Bcc)
					message.Bcc.Add(address.Trim());

				message.Subject = email.Subject;
				message.Body = email.Body;
				message.IsBodyHtml = false;

				using (var smtpClient = new SmtpClient())
				{
					smtpClient.UseDefaultCredentials = account.UseDefaultCredentials;
					smtpClient.Host = account.Host;
					smtpClient.Port = account.Port;
					smtpClient.EnableSsl = account.EnableSsl;
					if (account.UseDefaultCredentials)
						smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
					else
						smtpClient.Credentials = new NetworkCredential(account.Username, account.Password);

					smtpClient.Send(message);
				}
			}
		}
	}
}
