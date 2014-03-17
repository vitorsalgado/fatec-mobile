using System;

namespace Fatec.Core.Domain
{
	public class ApplicationEvent : AbstractEntity
	{
		public DateTime Date { get; set; }
		public string EventType { get; set; }
		public string Event { get; set; }

		public ApplicationEvent()
		{
			Date = DateTime.Now;
			EventType = "NONE";
		}
	}
}
