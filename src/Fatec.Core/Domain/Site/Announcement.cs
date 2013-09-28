﻿using System;

namespace Fatec.Core.Domain
{
	public class Announcement : AuditedEntity
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public DateTime DueDate { get; set; }
	}
}