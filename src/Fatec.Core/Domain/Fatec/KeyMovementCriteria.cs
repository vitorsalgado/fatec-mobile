namespace Fatec.Core.Domain
{
	public class KeyMovementCriteria : Criteria
	{
		public string Requester { get; set; }
		public string Key { get; set; }
	}
}
