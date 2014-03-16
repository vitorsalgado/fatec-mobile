using System;

namespace Fatec.Core.Domain
{
	public class Log : AbstractEntity
	{
		public string Message { get; set; }
		public string Details { get; set; }
		public string RawUrl { get; set; }
		public string IpAddress { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}