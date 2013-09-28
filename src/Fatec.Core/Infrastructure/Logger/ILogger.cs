using Fatec.Core.Domain;

namespace Fatec.Core.Infrastructure.Logger
{
	public interface ILogger
	{
		void Log(Log logEntry);
		void Warn(string warning);
		void Debug(string debug); 
	}
}
