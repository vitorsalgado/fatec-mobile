using System;
using System.Web.Mvc;

namespace Fatec.Core
{
	public class FatecEngine : IEngine
	{
		public object Resolve(Type type)
		{
			return DependencyResolver.Current.GetService(type);
		}

		public T Resolve<T>() where T : class
		{
			return DependencyResolver.Current.GetService<T>();
		}
	}
}
