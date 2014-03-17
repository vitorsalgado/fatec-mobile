using System;

namespace Fatec.Core.Domain
{
	[Serializable]
	public class Log : AbstractEntity
	{
		public string Message { get; set; }
		public string Url { get; set; }
		public string IpAddress { get; set; }
		public string Username { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Details { get; set; }
	}
}
