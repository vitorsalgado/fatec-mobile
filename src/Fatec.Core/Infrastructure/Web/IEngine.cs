using System;

namespace Fatec.Core
{
	public interface IEngine
	{
		object Resolve(Type type);
		T Resolve<T>() where T : class;
	}
}