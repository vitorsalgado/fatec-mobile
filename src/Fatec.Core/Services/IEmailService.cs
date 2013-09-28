using Fatec.Core.Domain;

namespace Fatec.Core.Services
{
	public interface IEmailService
	{
		void SendEmail(EmailAccount fromAccount, Email email);
	}
}
