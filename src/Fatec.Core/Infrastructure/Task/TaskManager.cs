using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fatec.Core.Infrastructure.Task
{
	public static class TaskManager
	{
		private static object _locker = new object();
		private static bool _startupTasksExecuted = false;

		public static void RunStartupTasks()
		{
			if (_startupTasksExecuted)
				return;

			lock (_locker)
			{
				var startupTasks = new List<ITask>();

				var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Fatec")).ToList();
				List<Type> startupTasksDefinition = new List<Type>();

				assemblies.ForEach((Assembly assembly) =>
				{
					startupTasksDefinition.AddRange(assembly.GetTypes()
						.Where(type => typeof(ITask).IsAssignableFrom(type) && type.IsClass));
				});

				foreach (var startupTaskDefinition in startupTasksDefinition)
					startupTasks.Add((ITask)Activator.CreateInstance(startupTaskDefinition));

				startupTasks.ToList().ForEach(x => x.Run());

				_startupTasksExecuted = true;
			}
		}
	}
}
