using System;

namespace Fatec.Core.Domain
{
	public class Log
	{
		public string Message { get; set; }
		public string Details { get; set; }
		public Exception Exception { get; set; }
		public int LevelId { get; set; }
		public LogLevel LogLevel
		{
			get { return (LogLevel)this.LevelId; }
			set { this.LevelId = (int)value; }
		}
		public string ReferrerUrl { get; set; }
		public string RawUrl { get; set; }
		public string IpAddress { get; set; }
		public int UserId { get; set; }
		public SysUser User { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}