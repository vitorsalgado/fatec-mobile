using System;

namespace Fatec.Core.Domain
{
	public class ApplicationActionLog : AbstractEntity
	{
		public DateTime Date { get; set; }
		public ActionType ActionType { get; set; }
		public string Message { get; set; }

		public ApplicationActionLog()
		{
			Date = DateTime.Now;
			ActionType = ActionType.NONE;
		}
	}
}
