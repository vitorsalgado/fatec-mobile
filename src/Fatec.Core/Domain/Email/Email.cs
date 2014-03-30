using System.Collections.Generic;

namespace Fatec.Core.Domain
{
	public class Email
	{
		public EmailPriority Priority { get; set; }
		public string From { get; set; }
		public IEnumerable<string> To { get; set; }
		public string Subject { get; set; }
		public IEnumerable<string> Cc { get; set; }
		public IEnumerable<string> Bcc { get; set; }
		public string Body { get; set; }

		public Email()
		{
			To = new List<string>();
			Cc = new List<string>();
			Bcc = new List<string>();
		}
	}
}
