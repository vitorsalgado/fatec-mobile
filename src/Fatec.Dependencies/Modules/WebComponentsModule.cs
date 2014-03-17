using Autofac;
using Autofac.Integration.Mvc;
using Fatec.Core;
using Fatec.Core.Infrastructure;
using System;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Fatec.Dependencies.Modules
{
	public class WebComponentsModule : Autofac.Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException("builder");

			builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase).As<HttpContextBase>().InstancePerHttpRequest();
			builder.RegisterControllers(GetAssemblies());
			builder.RegisterType<FatecWorkContext>().As<IWorkContext>().InstancePerHttpRequest();
		}

		private static Assembly[] GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.AsQueryable()
				.Where(x => x.FullName.Contains("Fatec"))
				.ToArray();
		}
	}
}
