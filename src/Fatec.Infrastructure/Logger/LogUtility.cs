using Fatec.Core.Domain;
using System.Text;

namespace Fatec.Infrastructure.Logger
{
	public static class LogUtility
	{
		public static string BuildHtmlLog(Log logEntry)
		{
			StringBuilder html = new StringBuilder();

			html.Append("<div style='padding:2px; border:1px #CCCCCC dotted; font-family:courier; margin:5px 0;'>")
				.Append("<h2 style='font-size:22px; margin:5px 0;'>").Append(logEntry.Message).Append("</h2>")
				.Append("<h3 style='font-size:18px; margin:5px 0;'>").Append("<span style='color:#C63717; font-weight:bold;'>").Append(logEntry.CreatedOn).Append("</span>").Append("</h3>")
				.Append("<div style='margin:3px 0;'>")
					.Append("<span style='font-weight:bold;'>Page Url:</span>").Append("<span style='color:#3366CC'>").Append(logEntry.RawUrl).Append("</span>")
					.Append("</br>")
					.Append("<span style='font-weight:bold;'>Username:</span>").Append("<span style='color:#C63717'>").Append(logEntry.Username).Append("</span>")
					.Append("</br>")
					.Append("<span style='font-weight:bold;'>IP Address:</span>").Append("<span style='color:#C63717'>").Append(logEntry.IpAddress).Append("</span>")
					.Append("</br>")
				.Append("</div>")
				.Append("<p style='background-color:#EBEBEB; margin:8px 0; padding:6px;'>")
					.Append("<span style='font-weight:bold;'>Exception Message:</span><span>").Append(logEntry.Message).Append("</span>")
				.Append("</p>")
				.Append("<p style='margin:5px 0;'>").Append(logEntry.Details.ToString()).Append("</p>")
			.Append("</div>");

			return html.ToString();
		}

		public static string BuildSimpleMessage(Log logEntry)
		{
			StringBuilder message = new StringBuilder();
			message.AppendLine()
				.Append("Raw Url: ").AppendLine(logEntry.RawUrl)
				.Append("Message: ").AppendLine(logEntry.Message)
				.Append("Exception: ").AppendLine().AppendLine(logEntry.Details)
			.AppendLine();
			return message.ToString();
		}
	}
}
