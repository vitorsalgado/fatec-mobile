using System;

namespace Fatec.Core.Domain
{
	public class KeyMovement : AbstractAuditedEntity
	{
		public string Requester { get; set; }
		public string Key { get; set; }
		public DateTime WithdrawalDate { get; set; }
	}
}
