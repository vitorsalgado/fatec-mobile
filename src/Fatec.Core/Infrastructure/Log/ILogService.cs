using Fatec.Core.Domain;

namespace Fatec.Core.Infrastructure.Logger
{
	public interface ILogService
	{
		void Log(Log logEntry);
		void Inform(string message);
		void Debug(string debug);
	}
}
