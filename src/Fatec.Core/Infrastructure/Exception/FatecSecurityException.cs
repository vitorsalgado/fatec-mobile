using System;
using System.Runtime.Serialization;

namespace Fatec.Core.Infrastructure
{
	public class FatecSecurityException : Exception
	{
		public FatecSecurityException() { }
		public FatecSecurityException(string message) : base(message) { }
		protected FatecSecurityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
		public FatecSecurityException(string message, Exception innerException) : base(message, innerException) { }

	}
}
