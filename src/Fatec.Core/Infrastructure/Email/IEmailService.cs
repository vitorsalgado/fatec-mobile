using Fatec.Core.Domain;

namespace Fatec.Core.Infrastructure.Mail
{
	public interface IEmailService
	{
		void SendEmail(EmailAccount fromAccount, Email email);
	}
}
