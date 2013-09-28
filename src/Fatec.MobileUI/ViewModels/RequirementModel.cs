using System;

namespace Fatec.MobileUI.ViewModels
{
	public class RequirementModel
	{
		public int Id { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public string Comments { get; set; }
		public DateTime EndDate { get; set; }
		public string Result { get; set; }
	}
}