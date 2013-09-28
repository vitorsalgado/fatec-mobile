using System;
using System.Runtime.Serialization;

namespace Fatec.Core.Infrastructure
{
	[Serializable]
	public class FatecException : Exception
	{
		public FatecException() { }
		public FatecException(string message) : base(message) { }
		protected FatecException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		public FatecException(string message, Exception innerException) : base(message, innerException) { }
	}
}
