using Fatec.Core.Domain;
using Fatec.Core.Infrastructure;
using Fatec.Core.Infrastructure.Logger;
using NLog;
using NLog.Config;
using System;

namespace Fatec.Infrastructure.Logger
{
	public class NLogLogger : ILogger
	{
		private static NLog.Logger _logger;
		private static string _logConfigPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\nlog.config");

		public NLogLogger()
		{
			var config = new XmlLoggingConfiguration(_logConfigPath, false);
			if (config == null)
				throw new FatecException("nlog.config was not found.");

			LogManager.Configuration = config;

			_logger = LogManager.GetCurrentClassLogger();
			_logger.Factory.Configuration = config;
		}

		public void Log(Log logEntry)
		{
			if (logEntry == null) throw new ArgumentNullException("logEntry");
			_logger.Error(LogUtility.BuildHtmlLog(logEntry));
		}

		public void Warn(string warning)
		{
			_logger.Info(warning);
		}

		public void Debug(string debug)
		{
			_logger.Debug(debug);
		}
	}
}
