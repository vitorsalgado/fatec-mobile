using Fatec.Core.Domain;
using Fatec.Core.Infrastructure.Logger;
using Fatec.Core.Repositories;
using System;

namespace Fatec.Infrastructure.Logger
{
	public class LogService : ILogService
	{
		private readonly ILogRepository _logRepository;
		private readonly IApplicationEventsRepository _applicationEventRepository;

		public LogService(ILogRepository logRepository, IApplicationEventsRepository applicationEventRepository)
		{
			if (logRepository == null) throw new ArgumentNullException("logRepository");
			if (applicationEventRepository == null) throw new ArgumentNullException("applicationEventRepository");

			_logRepository = logRepository;
			_applicationEventRepository = applicationEventRepository;
		}

		public void Log(Log logEntry)
		{
			if (logEntry == null) throw new ArgumentNullException("logEntry");
			_logRepository.Save(logEntry);
		}

		public void Warn(string warning)
		{
			if (string.IsNullOrEmpty(warning)) throw new ArgumentNullException("warning");

			ApplicationEvent evt = new ApplicationEvent();
			evt.EventType = "WARN";
			evt.Event = warning;

			_applicationEventRepository.Save(evt);
		}

		public void Debug(string debug)
		{
			if (string.IsNullOrEmpty(debug)) throw new ArgumentNullException("debug");

			ApplicationEvent evt = new ApplicationEvent();
			evt.EventType = "DEBUG";
			evt.Event = debug;

			_applicationEventRepository.Save(evt);
		}

		public void Inform(string message)
		{
			if (string.IsNullOrEmpty(message)) throw new ArgumentNullException("message");

			ApplicationEvent evt = new ApplicationEvent();
			evt.EventType = "INFORM";
			evt.Event = message;

			_applicationEventRepository.Save(evt);
		}
	}
}
